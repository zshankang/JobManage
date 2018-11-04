using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManage.Model
{
    /// <summary>
    /// Job信息表
    /// </summary>
    public class Job_Info : BaseEntity
    {
        private int _id = 0;
        private string _job_name;
        private string _job_assembly;
        private string _job_class;
        private string _job_corn;
        private int _job_type = 1;
        private int _job_execount = 0;
        private DateTime _job_starttime;
        private int _job_state = 0;
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
        /// 执行的方式的dll名称
        /// </summary>
        public string Job_assembly { get => _job_assembly; set => _job_assembly = value; }
        /// <summary>
        /// 执行的方法类
        /// </summary>
        public string Job_class { get => _job_class; set => _job_class = value; }
        /// <summary>
        /// 执行任务的corn表达式
        /// </summary>
        public string Job_corn { get => _job_corn; set => _job_corn = value; }
        /// <summary>
        /// 任务类型，默认为1 简单，2 复杂
        /// </summary>
        public int Job_type { get => _job_type; set => _job_type = value; }
        /// <summary>
        /// 任务的执行总次数,0表示无限次
        /// </summary>
        public int Job_execount { get => _job_execount; set => _job_execount = value; }
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime Job_starttime { get => _job_starttime; set => _job_starttime = value; }
        /// <summary>
        /// 任务的状态 0 准备中，1 执行中,2 暂定，3 停止，4 结束
        /// </summary>
        public int Job_state { get => _job_state; set => _job_state = value; }
        /// <summary>
        /// 任务的创建时间
        /// </summary>
        public DateTime Addtime { get => _addtime; set => _addtime = value; }
        
    }
}
