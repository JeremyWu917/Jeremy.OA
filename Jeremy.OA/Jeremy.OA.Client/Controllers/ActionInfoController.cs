using Jeremy.OA.IBLL;
using Jeremy.OA.Model;
using Jeremy.OA.Model.EnumType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jeremy.OA.Client.Controllers
{
    public class ActionInfoController : BaseController
    {
        // GET: ActionInfo
        IActionInfoService ActionInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region 获取权限信息
        /// <summary>
        /// 获取权限列表信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetActionInfoList()
        {
            // 当前页的索引，默认显示第一页
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            // 每页显示的条数，默认显示5条
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            // 接收搜索条件
            string userName = Request["name"];
            string userRemark = Request["remark"];
            // 总页数，用来接收返回的总页数
            int totalCount = 0;
            int delFlag = (short)DeleteEnumType.Normal;

            var actionInfoList = ActionInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, r => r.DelFlag == delFlag, r => r.ID, true);
            var temp = from r in actionInfoList
                       select new { ID = r.ID, Url = r.Url, HttpMethod = r.HttpMethod, ActionMethodName = r.ActionMethodName, ControllerName = r.ControllerName, ActionTypeEnum = r.ActionTypeEnum, MenuIcon = r.MenuIcon, IconWidth = r.IconWidth, IconHeight = r.IconHeight, ActionInfoName = r.ActionInfoName, DelFlag = r.DelFlag, Sort = r.Sort, SubTime = r.SubTime, Remark = r.Remark };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除权限信息
        /// <summary>
        /// 删除权限列表信息
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteActionInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            // 将Id的格式从string转换成int
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            // 将list集合存储的要删除的记录的编号传递到业务层
            if (ActionInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 添加权限数据
        public ActionResult AddActionInfo(ActionInfo actionInfo)
        {
            actionInfo.DelFlag = 0;
            actionInfo.ModifiedOn = DateTime.Now.ToString();
            actionInfo.SubTime = DateTime.Now;
            ActionInfoService.AddEntity(actionInfo);
            return Content("ok");
        }
        #endregion

        #region 展示要修改的数据
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
            var actionInfo = ActionInfoService.LoadEntities(u => u.ID == id).FirstOrDefault();
            return Json(actionInfo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 提交修改的数据
        public ActionResult EditActionInfo(ActionInfo actionInfo)
        {
            actionInfo.ModifiedOn = DateTime.Now.ToString();
            if (ActionInfoService.EditEntity(actionInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

    }
}