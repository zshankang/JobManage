using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace JobManage.Job
{
    public class JobConfig
    {

        /// <summary>
        /// 静态实例 调度器
        /// </summary>
        public static IScheduler scheduler = null;

        /// <summary>
        /// 静态实例 调度器工厂
        /// </summary>
        public static ISchedulerFactory factory = null;

        /// <summary>
        /// Job_Key后缀
        /// </summary>
        public const string JOBKEY_NAMEEND = "_Job";
        /// <summary>
        /// Trigger后缀
        /// </summary>
        public const string JOBTRIGGER_NAMEEND = "_Trigger";
    }
}
