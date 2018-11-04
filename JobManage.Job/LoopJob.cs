using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManage.Model;
using JobManage.BLL;
using JobManage.Log;

namespace JobManage.Job
{
    /// <summary>
    /// 基层JOB，用于检查Job_Info表
    /// </summary>
    public class LoopJob : IJob
    {
        string logger = "LoopJob";
        /// <summary>
        /// 执行JOB 用于检查Job_Info表
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            Log4NetHelper.Info("开始获取JobInfo", logger: logger);
            try
            {
                List<Job_Info> jobList = new JobInfoBLL().GetJobList();
                if (jobList != null && jobList.Count > 0)
                {
                    JobKey jobKey = null;
                    foreach (var jobinfo in jobList)
                    {
                        jobKey = new JobKey(jobinfo.Job_name + JobConfig.JOBKEY_NAMEEND, "group1");
                        //只有正常执行状态的Job才添加到调度器中
                        if (!JobConfig.scheduler.CheckExists(jobKey) && jobinfo.Job_state == 0)
                        {
                            IJobDetail job = JobBuilder.Create<RunTaskDLLJob>().WithIdentity(jobKey).Build();

                            //创建触发器
                            TriggerBuilder tb = TriggerBuilder.Create()
                            .WithIdentity(jobinfo.Job_name + JobConfig.JOBTRIGGER_NAMEEND, "group1");
                            if (!string.IsNullOrEmpty(jobinfo.Job_corn))
                                tb.WithCronSchedule(jobinfo.Job_corn);//执行corn表达式
                            if (jobinfo.Job_execount > 0)//如果执行固定的次数
                                tb.WithSimpleSchedule(a => a.WithRepeatCount(jobinfo.Job_execount));

                            if (jobinfo.Job_starttime != null && jobinfo.Job_starttime > DateTime.Now)//设置Job启动时间
                                tb.StartAt(jobinfo.Job_starttime);
                            else
                                tb.StartNow();

                            ITrigger trigger = tb.Build();
                            //传递参数
                            job.JobDataMap.Add(jobKey.Name, jobinfo.Id);

                            JobConfig.scheduler.ScheduleJob(job, trigger);

                            Log4NetHelper.Info(string.Format("加入Job:{0}成功", jobKey.Name), logger: logger);
                        }
                        else if (JobConfig.scheduler.CheckExists(jobKey))
                        {
                            if (jobinfo.Job_state == 2 || jobinfo.Job_state == 3)
                            {
                                JobConfig.scheduler.PauseJob(jobKey);
                                Log4NetHelper.Info(string.Format("暂停Job:{0}成功", jobKey.Name), logger: logger);
                            }
                            else if (jobinfo.Job_state == 4)
                            {
                                JobConfig.scheduler.DeleteJob(jobKey);
                                Log4NetHelper.Info(string.Format("删除Job:{0}成功", jobKey.Name), logger: logger);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Info(string.Format("LoopJob异常：{0},{1},{2}", ex.Message, ex.InnerException, ex.StackTrace), logger: logger);
            }
            Log4NetHelper.Info("结束获取JobInfo", logger: logger);
        }

    }
}
