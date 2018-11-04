using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobManage.Model;
using JobManage.BLL;

namespace JobManage.Web.Controllers
{
    public class JobController : BaseController
    {
      
        public ActionResult List(Pager pager)
        {
            Result<object> result = new Result<object>();
            try
            {
                var jbll = new JobInfoBLL();
                var TotalCount = jbll.GetJobCount();

                var PageCount = (TotalCount % pager.PageSize) == 0 ? (TotalCount / pager.PageSize) : ((TotalCount / pager.PageSize) + 1);

                List<Job_Info> list = jbll.GetJobList(pager.PageIndex, pager.PageSize);

                result.Data = list;
                result.PageIndex = pager.PageIndex;
                result.PageSize = pager.PageSize;
                result.TotalCount = TotalCount;
                result.TotalPage = PageCount;
            }
            catch (Exception ex)
            {

            }
            return Success(result);
        }
        
        public ActionResult Detail(int id = 0)
        {
            return Form();
        }
        
        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(Job_Info job)
        {
            var bll = new JobInfoBLL();
            if(bll.AddInfo(job) > 0)
            {
                return Success();
            }
            else
            {
                return Error();
            }
        }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(Job_Info job)
        {
            var bll = new JobInfoBLL();
            if(bll.UpdateJobState(job) > 0)
            {
                return Success();
            }
            else
            {
                return Error();
            }
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetOne(int id)
        {
            var result = new JobInfoBLL();
            var job = result.GetOne(id);
            return Success(data: job);
        }

    }
}