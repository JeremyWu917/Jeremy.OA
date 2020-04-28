using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jeremy.OA.Client.Controllers
{
    public class HomeController : BaseController//Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // 系统主页
        public ActionResult HomeIndex()
        {
            return View();
        }
    }
}