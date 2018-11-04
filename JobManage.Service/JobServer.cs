using JobManage.Job;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace JobManage.Service
{
    /// <summary>
    /// Job 托管服务
    /// </summary>
    public class JobServer : ServiceControl, ServiceSuspend
    {
        public bool Continue(HostControl hostControl)
        {
            if (JobConfig.factory != null)
            {
                JobConfig.scheduler.ResumeAll();
            }
            return true;
        }

        public bool Pause(HostControl hostControl)
        {
            if (JobConfig.factory != null)
            {
                JobConfig.scheduler.PauseAll();
            }
            return true;
        }

        public bool Start(HostControl hostControl)
        {
            if (JobConfig.factory == null)
            {
                JobConfig.factory = new StdSchedulerFactory();
                JobConfig.scheduler = JobConfig.factory.GetScheduler();
                JobConfig.scheduler.Start();

                //创建循环Job
                IJobDetail job = JobBuilder.Create<LoopJob>().WithIdentity("LoopJob", "group1").Build();
                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("LoopJobtrigger", "group1")
                .WithCronSchedule("0 0/5 * * * ?")     //30分种执行一次
                .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(5)))
                .Build();

                JobConfig.scheduler.ScheduleJob(job, trigger);
            }
            else
            {
                JobConfig.scheduler.Start();
            }
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            if (JobConfig.factory != null)
            {
                JobConfig.scheduler.Shutdown(false);
                JobConfig.factory = null;
                JobConfig.scheduler = null;
            }
            return true;
        }
    }
}
