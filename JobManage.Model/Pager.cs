using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JobManage.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Pager
    {
        public Pager()
        {
            var dic = HttpContext.Current.Items["Params"] as Dictionary<String, String>;

            // 这里必须用可空字典，否则直接通过索引查不到数据时会抛出异常
           var tempdic = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
            var nvss = new NameValueCollection[] { HttpContext.Current.Request.QueryString, HttpContext.Current.Request.Form };
            foreach (var nvs in nvss)
            {
                foreach (var item in nvs.AllKeys)
                {
                    if (string.IsNullOrWhiteSpace(item)) continue;
                    if (item.StartsWith("__VIEWSTATE", true, System.Globalization.CultureInfo.CurrentCulture)) continue;

                    // 空值不需要
                    var value = nvs[item];
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        // 如果请求字符串里面有值而后面表单为空，则抹去
                        if (tempdic.ContainsKey(item)) tempdic.Remove(item);
                        continue;
                    }

                    // 同名时优先表单
                    tempdic[item] = value.Trim();
                }
            }
            param = tempdic;
            HttpContext.Current.Items["Params"] = tempdic;
        }
        private int _pageIndex = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { set => _pageIndex = value; get => _pageIndex; }
        private int _pageSize = 15;
        /// <summary>
        /// 每页的数量
        /// </summary>
        public int PageSize { set => _pageSize = value; get => _pageSize; }

        private Dictionary<string, string> param;
        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string,string> Params { get => param; }
    }
}
