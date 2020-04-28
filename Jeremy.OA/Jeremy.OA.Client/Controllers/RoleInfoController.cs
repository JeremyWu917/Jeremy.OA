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
    public class RoleInfoController : BaseController
    {
        // GET: RoleInfo
        IRoleInfoService RoleInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region 获取角色列表数据
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleInfoList()
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
            var roleInfoList = RoleInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, r => r.DelFlag == delFlag, r => r.ID, true);
            var temp = from r in roleInfoList
                       select new { ID = r.ID, RoleName = r.RoleName, DelFlag = r.DelFlag, Sort = r.Sort, SubTime = r.SubTime, Remark = r.Remark };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 删除角色数据
        public ActionResult DeleteRoleInfo()
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
            if (RoleInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 添加角色数据
        public ActionResult AddRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.DelFlag = 0;
            roleInfo.ModifiedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;
            RoleInfoService.AddEntity(roleInfo);
            return Content("ok");
        }
        #endregion

        #region 展示要修改的数据
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
            var uroleInfo = RoleInfoService.LoadEntities(u => u.ID == id).FirstOrDefault();
            return Json(uroleInfo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 提交修改的数据
        public ActionResult EditRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.ModifiedOn = DateTime.Now;
            if (RoleInfoService.EditEntity(roleInfo))
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