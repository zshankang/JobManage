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
    /// Job信息逻辑类
    /// </summary>
    public class JobInfoBLL
    {
        /// <summary>
        /// 添加Job信息
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public int AddInfo(Job_Info jobInfo)
        {
            int result = 0;

            if (jobInfo == null)
                return result;
            try
            {
                string SqlStr = string.Format("insert into job_info(job_name,job_assembly,job_class,job_corn,job_type,job_execount,job_starttime,job_state) values('{0}','{1}','{7}','{2}',{3},{4},'{5}',{6})",
                    jobInfo.Job_name, jobInfo.Job_assembly, jobInfo.Job_corn, jobInfo.Job_type, jobInfo.Job_execount, jobInfo.Job_starttime.ToString("yyyy-MM-dd HH:mm:ss"), 0, jobInfo.Job_class);
                result = MySqlHelper.ExceuteSql(SqlStr);
            }
            finally
            {

            }

            return result;
        }

        /// <summary>
        /// 更新Job的状态
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public int UpdateJobState(Job_Info jobInfo)
        {
            int result = 0;

            if (jobInfo == null)
                return result;
            try
            {
                string SqlStr = string.Format("update job_info set job_state={0} where ", jobInfo.Job_state);
                if(jobInfo.Id > 0)
                {
                    SqlStr += string.Format(" id={0};", jobInfo.Id);
                }else if (!string.IsNullOrEmpty(jobInfo.Job_name))
                {
                    SqlStr += string.Format(" job_name='{0}'", jobInfo.Job_name);
                }
                result = MySqlHelper.ExceuteSql(SqlStr);
            }
            finally
            {

            }

            return result;
        }

        /// <summary>
        /// job列表
        /// </summary>
        /// <returns></returns>
        public List<Job_Info> GetJobList()
        {
            List<Job_Info> list = null;

            try
            {
                string SqlStr = "select * from job_info";
                var ds = MySqlHelper.GetDataSet(SqlStr);
                if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    list = new List<Job_Info>();
                    Job_Info info = new Job_Info();
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        info = Job_Info.ToEntity<Job_Info>(row, info);
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
        /// job列表
        /// </summary>
        /// <returns></returns>
        public List<Job_Info> GetJobList(int pageIndex,int pageSize)
        {
            List<Job_Info> list = null;

            try
            {
                string SqlStr = "select * from job_info";
                SqlStr += string.Format(" limit {0},{1};", (pageIndex - 1) * pageSize, pageSize);
                var ds = MySqlHelper.GetDataSet(SqlStr);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    list = new List<Job_Info>();
                    Job_Info info = new Job_Info();
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        info = Job_Info.ToEntity<Job_Info>(row, info);
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

        public Job_Info GetOne(int id)
        {
            Job_Info job = null;

            try
            {
                string SqlStr = "select * from job_info where id="+id;
                var ds = MySqlHelper.GetDataSet(SqlStr);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    job = new Job_Info();
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        job = Job_Info.ToEntity<Job_Info>(row, job);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return job;
        }

        /// <summary>
        /// job数量
        /// </summary>
        /// <returns></returns>
        public int GetJobCount()
        {
           int list = 0;

            try
            {
                string SqlStr = "select count(0) from job_log";
                var ds = MySqlHelper.GetReader(SqlStr);
                if (ds != null)
                {
                    int.TryParse(ds.ToString(), out list);
                }
            }
            finally
            {

            }

            return list;
        }

    }
}
