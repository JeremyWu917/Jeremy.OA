using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jeremy.OA.Client.Models
{
    /// <summary>
    /// 异常处理，捕获异常数据
    /// </summary>
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        public static Queue<Exception> ExceptionsQueue = new Queue<Exception>();

        /// <summary>
        /// 捕获异常数据
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception ex = filterContext.Exception;
            // 将异常数据写入到队列中
            ExceptionsQueue.Enqueue(ex);
            // 跳转到错误页面
            filterContext.HttpContext.Response.Redirect("/Error.html");
        }
    }
}