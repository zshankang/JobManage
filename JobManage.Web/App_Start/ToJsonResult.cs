using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobManage.Web.App_Start
{
    public class ToJsonResult : JsonResult
    {
        const string error = "该请求已被封锁，因为敏感信息透露给第三方网站，这是一个GET请求时使用的。为了可以GET请求，请设置JsonRequestBehavior AllowGet。";

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(error);
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!string.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
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
                string jsonstring = JsonConvert.SerializeObject(Data, Formatting.None, setting);
                response.Write(jsonstring);
            }
            else
                response.Write("null");
        }
    }
}