using JobManage.BLL;
using JobManage.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace JobManage.Job
{
    public class RunTaskDLLJob : IJob
    {
        JobInfoBLL jobbll = new JobInfoBLL();
        Job_Info job = null;
        public void GetJob(int id)
        {
            job = jobbll.GetOne(id);
        }

        public void Execute(IJobExecutionContext context)
        {
            if (context.JobDetail.JobDataMap[context.JobDetail.Key.Name] != null)
            {
                GetJob(Convert.ToInt32(context.JobDetail.JobDataMap[context.JobDetail.Key.Name].ToString()));
            }
            if (job == null)
                return;
            //如果已暂定或停止的，不再执行
            if (job.Job_state >= 2)
                return;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int state = 0;
            Exception exce = null;
            string jobname = job.Job_name;
            DateTime dt = DateTime.Now;
            long ts = 0;
            try
            {
                Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + job.Job_assembly);
                object obj = assembly.CreateInstance(job.Job_class);

                Lib.IJob jobi = (Lib.IJob)obj;
                //jobi.JobLog += Jobi_JobLog;

                jobbll.UpdateJobState(new Job_Info() { Job_state = 1, Id = job.Id });

                var r = jobi.Exceute();
                //if (!r)
                //    state = 1;
            }
            catch (Exception ex)
            {
                exce = ex;
                state = 1;
            }
            finally
            {
                jobbll.UpdateJobState(new Job_Info() { Job_state = 0, Id = job.Id });
                stopwatch.Stop();
                ts = stopwatch.ElapsedMilliseconds;

                this.Job_JobLog(state, jobname, exce, dt, ts);
            }

        }

        /// <summary>
        /// 记录 Job 日志，参数说明 按顺序：执行结果，0正常 1异常；Job名称；异常信息；开始执行时间；耗时 单位ms
        /// </summary>
        /// <param name="state"></param>
        /// <param name="jobname"></param>
        /// <param name="exce"></param>
        /// <param name="dt"></param>
        /// <param name="ts"></param>
        private void Job_JobLog(int state, string jobname, Exception exce, DateTime dt, long ts)
        {
            new JobLogBLL().AddLog(new Job_Log()
            {
                Job_exception = exce == null ? null : exce.Message + ";" + exce.InnerException + ";" + exce.StackTrace,
                Job_exedate = dt,
                Job_exestate = state,
                Job_exetime = Convert.ToInt32(ts),
                Job_name = jobname,
                Job_result = state == 0 ? "执行成功" : "执行异常"
            });
        }

    }
}
