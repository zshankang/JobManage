using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManage.Model;
using JobManage.Utity;

namespace JobManage.BLL
{
    /// <summary>
    /// Job 日志逻辑类
    /// </summary>
    public class JobLogBLL
    {

        /// <summary>
        /// 添加Job日志信息
        /// </summary>
        /// <param name="jobLog"></param>
        /// <returns></returns>
        public int AddLog(Job_Log jobLog)
        {
            int result = 0;

            if (jobLog == null)
                return result;
            try
            {
                string SqlStr = string.Format("insert into job_log(job_name,job_result,job_exception,job_exetime,job_exedate,job_exestate) values('{0}','{1}','{2}',{3},'{4}',{5})",
                    jobLog.Job_name, jobLog.Job_result, jobLog.Job_exception, jobLog.Job_exetime, jobLog.Job_exedate.ToString("yyyy-MM-dd HH:mm:ss"), jobLog.Job_exestate);
                result = MySqlHelper.ExceuteSql(SqlStr);
            }
            finally
            {

            }

            return result;
        }
        
        /// <summary>
        /// job日志列表
        /// </summary>
        /// <returns></returns>
        public List<Job_Log> GetJobLogList()
        {
            List<Job_Log> list = null;

            try
            {
                string SqlStr = "select * from job_log";
                var ds = MySqlHelper.GetDataSet(SqlStr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = new List<Job_Log>();
                    Job_Log info = new Job_Log();
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        info = Job_Log.ToEntity<Job_Log>(row, info);
                        if (info != null)
                            list.Add(info);
                    }
                }
            }
            finally
            {

            }

            return list;
        }

        /// <summary>
        /// job日志列表
        /// </summary>
        /// <returns></returns>
        public List<Job_Log> GetJobLogList(int pageIndex, int pageSize)
        {
            List<Job_Log> list = null;

            try
            {
                string SqlStr = "select * from job_log";
                SqlStr += string.Format(" limit {0},{1};", (pageIndex - 1) * pageSize, pageSize);
                var ds = MySqlHelper.GetDataSet(SqlStr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = new List<Job_Log>();
                    Job_Log info = new Job_Log();
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        info = Job_Log.ToEntity<Job_Log>(row, info);
                        if (info != null)
                            list.Add(info);
                    }
                }
            }
            finally
            {

            }

            return list;
        }

        public int GetJobLogCount(int state = -1)
        {
            int count = 0;

            try
            {
                string SqlStr = "select count(0) from job_log";
                if (state > -1)
                {
                    SqlStr += " where job_exestate=" + state;
                }
                var ds = MySqlHelper.GetReader(SqlStr);
                if (ds != null)
                {
                    int.TryParse(ds.ToString(), out count);
                }
            }
            finally
            {

            }

            return count;
        }

    }
}
