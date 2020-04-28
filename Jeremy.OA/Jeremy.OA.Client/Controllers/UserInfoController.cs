using Jeremy.OA.IBLL;
using Jeremy.OA.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jeremy.OA.Model.EnumType;
using Jeremy.OA.Model;
using Jeremy.OA.Model.Search;

namespace Jeremy.OA.Client.Controllers
{
    public class UserInfoController : BaseController//Controller
    {
        // GET: UserInfo
        IUserInfoService UserInfoService { get; set; }//= new UserInfoService();
        IRoleInfoService RoleInfoService { get; set; }
        IActionInfoService ActionInfoService { get; set; }
        IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region 获取用户列表数据
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserInfoList()
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
            // 构建搜索条件
            UserInfoSearch userInfoSearch = new UserInfoSearch()
            {
                UserName = userName,
                UserRemark = userRemark,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };
            // 删除标记索引
            int delFlag = (int)DeleteEnumType.Normal;
            // 根据构建好的搜索条件完成搜索
            var userInfoList = UserInfoService.LoadSearchEntites(userInfoSearch, delFlag);


            // 查询分页数据列表
            // var userInfoList = UserInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, u => u.DelFlag == delFlag, u => u.ID, true);

            // 筛选部分列
            var temp = from u in userInfoList
                       select new
                       {
                           ID = u.ID,
                           UName = u.UName,
                           UPwd = u.UPwd,
                           SubTime = u.SubTime,
                           Remark = u.Remark
                       };

            // 返回Json数据，数据给rows，总的记录数给total
            // easyui grid已经写死了rows和total
            //return Json(new { rows = temp, total = totalCount });
            return Json(new { rows = temp, total = userInfoSearch.TotalCount }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region 删除用户数据
        public ActionResult DeleteUserInfo()
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
            if (UserInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 添加用户数据
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
            userInfo.DelFlag = 0;
            userInfo.ModifiedOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            UserInfoService.AddEntity(userInfo);
            return Content("ok");
        }
        #endregion

        #region 展示要修改的数据
        /// <summary>
        /// 显示要修改的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
            var userInfo = UserInfoService.LoadEntities(u => u.ID == id).FirstOrDefault();
            return Json(userInfo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 提交修改的数据
        public ActionResult EditUserInfo(UserInfo userInfo)
        {
            userInfo.ModifiedOn = DateTime.Now;
            if (UserInfoService.EditEntity(userInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        #endregion

        #region 展示用户角色信息
        public ActionResult ShowUserRoleInfo()
        {
            int id = int.Parse(Request["id"]);
            var userInfo = UserInfoService.LoadEntities(u => u.ID == id).FirstOrDefault();
            ViewBag.UserInfo = userInfo;
            //查询所有的角色.
            int delFlag = (short)DeleteEnumType.Normal;
            var allRoleList = RoleInfoService.LoadEntities(r => r.DelFlag == delFlag).ToList();
            //查询一下要分配角色的用户以前具有了哪些角色编号。
            var allUserRoleIdList = (from r in userInfo.RoleInfo
                                     select r.ID).ToList();
            ViewBag.AllRoleList = allRoleList;
            ViewBag.AllUserRoleIdList = allUserRoleIdList;
            return View();
        }
        #endregion

        #region 完成用户角色的分配
        public ActionResult SetUserRoleInfo()
        {
            int userId = int.Parse(Request["userId"]);
            string[] allKeys = Request.Form.AllKeys;//获取所有表单元素name属性值。
            List<int> roleIdList = new List<int>();
            foreach (string key in allKeys)
            {
                if (key.StartsWith("cba_"))
                {
                    string k = key.Replace("cba_", "");
                    roleIdList.Add(Convert.ToInt32(k));
                }
            }
            if (UserInfoService.SetUserRoleInfo(userId, roleIdList))//设置用户的角色
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 展示权限信息
        public ActionResult ShowUserAction()
        {
            int userId = int.Parse(Request["userId"]);
            var userInfo = UserInfoService.LoadEntities(u => u.ID == userId).FirstOrDefault();
            ViewBag.UserInfo = userInfo;
            //获取所有的权限。
            short delFlag = (short)DeleteEnumType.Normal;
            var allActionList = ActionInfoService.LoadEntities(a => a.DelFlag == delFlag).ToList();
            //获取要分配的用户已经有的权限。
            var allActionIdList = (from a in userInfo.R_UserInfo_ActionInfo
                                   select a).ToList();
            ViewBag.AllActionList = allActionList;
            ViewBag.AllActionIdList = allActionIdList;
            return View();
        }
        #endregion

        #region 完成用户权限的分配
        public ActionResult SetUserAction()
        {
            int actionId = int.Parse(Request["actionId"]);
            int userId = int.Parse(Request["userId"]);
            bool isPass = Request["isPass"] == "true" ? true : false;
            if (UserInfoService.SetUserActionInfo(actionId, userId, isPass))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 完成权限删除
        public ActionResult ClearUserAction()
        {
            int actionId = int.Parse(Request["actionId"]);
            int userId = int.Parse(Request["userId"]);
            var r_userInfo_actionInfo = R_UserInfo_ActionInfoService.LoadEntities(r => r.ActionInfoID == actionId && r.UserInfoID == userId).FirstOrDefault();
            if (r_userInfo_actionInfo != null)
            {
                if (R_UserInfo_ActionInfoService.DeleteEntity(r_userInfo_actionInfo))
                {
                    return Content("ok:删除成功!!");
                }
                else
                {
                    return Content("ok:删除失败!!");
                }
            }
            else
            {
                return Content("no:数据不存在!!");
            }

        }
        #endregion

    }
}