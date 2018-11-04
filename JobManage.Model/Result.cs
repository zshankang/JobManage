using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManage.Model
{
    public class Result
    {
        private int _status = 1;
        /// <summary>
        /// 状态 0 表示成功，1 表示失败
        /// </summary>
        public int Status { set => _status = value; get => _status; }

        private string _message;
        public string Message { set => _message = value; get => _message; }

    }

    public class Result<T> : Result
    {
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

        public T Data { set; get; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { set; get; }
        /// <summary>
        /// 页的总数量
        /// </summary>
        public int TotalPage { set; get; }
    }
}
