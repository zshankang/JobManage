using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using log4net;

namespace JobManage.Service
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。 ！！！！！！！！注意：该项目必须为Console项目
        /// </summary>
        static void Main()
        {
            HostFactory.Run(x =>
            {
                x.SetServiceName("JobManage-WinServer");
                x.SetDisplayName("JobManage WinServer");
                x.SetDescription("JobManage WinServer");
                x.Service<JobServer>();

                x.EnablePauseAndContinue();
            });

        }
    }
}
