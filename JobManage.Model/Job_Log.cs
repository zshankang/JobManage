using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManage.Model
{
    /// <summary>
    /// Job运行日志表
    /// </summary>
    public class Job_Log : BaseEntity
    {
        private int _id = 0;
        private string _job_name;
        private string _job_result;
        private string _job_exception;
        private int _job_exetime;
        private DateTime _job_exedate;
        private int _job_exestate;
        private DateTime _addtime;

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get => _id; set => _id = value; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Job_name { get => _job_name; set => _job_name = value; }
        /// <summary>
        /// 执行的结果
        /// </summary>
        public string Job_result { get => _job_result; set => _job_result = value; }
        /// <summary>
        /// 执行任务的异常信息
        /// </summary>
        public string Job_exception { get => _job_exception; set => _job_exception = value; }
        /// <summary>
        /// 执行耗时，单位ms
        /// </summary>
        public int Job_exetime { get => _job_exetime; set => _job_exetime = value; }
        /// <summary>
        /// 任务的执行的日期时间
        /// </summary>
        public DateTime Job_exedate { get => _job_exedate; set => _job_exedate = value; }
        /// <summary>
        /// 执行结果 0 正常，1 异常
        /// </summary>
        public int Job_exestate { get => _job_exestate; set => _job_exestate = value; }
        /// <summary>
        /// 任务的执行时间
        /// </summary>
        public DateTime Addtime { get => _addtime; set => _addtime = value; }
    }
}
