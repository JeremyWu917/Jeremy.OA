using Jeremy.OA.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jeremy.OA.Client.Controllers
{
    public class LoginController : Controller
    {
        UserInfoService userInfoService { get; set; }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        #region 完成用户登录
        public ActionResult UserLogin()
        {
            string validateCode = Session["validateCode"] != null ? Session["validateCode"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误");
            }

            Session["validateCode"] = null;
            string txtCode = Request["vCode"];
            if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            var userInfo = userInfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();
            if (userInfo!=null)
            {
                Session["userInfo"] = userInfo;
                return Content("ok:登陆成功");
            }
            else
            {
                return Content("no:用户名或者密码错误");
            }
        }
        #endregion

        #region 显示验证码
        public ActionResult ShowValidateCode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            // 产生一个4位的验证码
            string code = validateCode.CreateValidateCode(4);
            // 保存Session
            Session["validateCode"] = code;
            // 保存验证码画布文件
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
        #endregion
    }
}