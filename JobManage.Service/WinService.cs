using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using JobManage.Job;
using Quartz;
using Quartz.Impl;

namespace JobManage.Service
{
    public partial class WinService : ServiceBase
    {
        public WinService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开始服务
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            if(JobConfig.factory == null)
            {
                JobConfig.factory = new StdSchedulerFactory();
                JobConfig.scheduler = JobConfig.factory.GetScheduler();

                //创建循环Job
                IJobDetail job = JobBuilder.Create<LoopJob>().WithIdentity("LoopJob", "group1").Build();
                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("LoopJobtrigger", "group1")
                .WithCronSchedule("0 30 * * * ?")     //30分种执行一次
                .StartNow()
                .Build();

                JobConfig.scheduler.ScheduleJob(job, trigger);

                JobConfig.scheduler.Start();
            }
            else
            {
                JobConfig.scheduler.Start();
            }
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        protected override void OnStop()
        {
            if(JobConfig.factory != null)
            {
                JobConfig.scheduler.Shutdown(false);
                JobConfig.factory = null;
                JobConfig.scheduler = null;
            }
        }
    }
}
