using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jeremy.OA.Client.Controllers
{
    public class TestController : BaseController//Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用来测试异常捕获
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowResult()
        {
            int a = 0;
            int c = 1 / a;
            return Content(c.ToString());
        }
    }
}