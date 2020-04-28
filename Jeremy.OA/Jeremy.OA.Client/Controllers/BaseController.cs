using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jeremy.OA.Client.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 执行控制器的方法之前先执行该方法。
        /// 让需要登陆后才能展示的页面集成BaseController即可
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["userInfo"] == null)
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                // 浏览器请求必须拿到一个ActionResult
                filterContext.Result = Redirect("/Login/Index");
            }
        }
    }
}