using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JobManage.Model;

namespace JobManage.Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Add(int id = 0)
        {
            return Form();
        }
        /// <summary>
        /// 操作成功 的返回
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual ActionResult Success(string Message = "操作成功", object data = null)
        {
            return Json(new Result<Object>{ Status = 0, Message = Message, Data = data }, JsonRequestBehavior.AllowGet);
        }
        public virtual ActionResult Success(Result<object> result)
        {
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 操作失败 的返回
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual ActionResult Error(string Message = "操作失败", object data = null)
        {
            return Json(new Result<Object> { Status = 1, Message = Message, Data = data }, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Form()
        {
            return View("Form");
        }

        /// <summary>
        /// 序列化json字符串
        /// </summary>
        /// <param name="data">序列化对象</param>
        /// <returns></returns>
        public string ToJson(object data)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            };
            setting.Converters.Clear();
            setting.Converters.Add(new IsoDateTimeConverter()
            {
                DateTimeFormat = "yyyy'-'MM'-'dd HH:mm:ss"
            });
            string jsonstring = JsonConvert.SerializeObject(data, Formatting.None, setting);

            return jsonstring;
        }

        public JsonResult Json(object data, JsonRequestBehavior behavior)
        {
            return new App_Start.ToJsonResult()
            {
                Data = data,
                ContentType = "application/json",
                ContentEncoding = UTF8Encoding.UTF8,
                JsonRequestBehavior = behavior
            };
        }

    }
}