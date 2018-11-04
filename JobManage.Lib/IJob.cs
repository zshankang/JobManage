using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManage.Lib
{
    /// <summary>
    /// 基Job
    /// </summary>
    public interface IJob
    {
        ///// <summary>
        ///// 记录 Job 日志，参数说明 按顺序：执行结果，0正常 1异常；Job名称；异常信息；开始执行时间；耗时 单位ms
        ///// </summary>
        //event Action<int, string, Exception, DateTime, long> JobLog;
        /// <summary>
        /// 执行任务
        /// </summary>
        bool Exceute();
    }
}
