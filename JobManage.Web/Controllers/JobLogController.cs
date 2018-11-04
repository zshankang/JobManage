using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobManage.Model;
using JobManage.BLL;

namespace JobManage.Web.Controllers
{
    public class JobLogController : BaseController
    {
        public ActionResult List(Pager pager)
        {
            Result<object> result = new Result<object>();
            try
            {
                var jbll = new JobLogBLL();
                var TotalCount = jbll.GetJobLogCount();

                var PageCount = (TotalCount % pager.PageSize) == 0 ? (TotalCount / pager.PageSize) : ((TotalCount / pager.PageSize) + 1);

                List<Job_Log> list = new JobLogBLL().GetJobLogList(pager.PageIndex, pager.PageSize);

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
    }
}