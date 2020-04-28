using Jeremy.OA.IBLL;
using Jeremy.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.BLL
{
    public partial class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
        /// <summary>
        /// 为角色分配权限
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="actionIdList">权限编号列表</param>
        /// <returns></returns>
        public bool SetRoleActionInfo(int roleId, List<int> actionIdList)
        {
            //获取角色信息.
            var roleInfo = this.CurrentDBSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
            if (roleInfo != null)
            {
                roleInfo.ActionInfo.Clear();
                foreach (int actionId in actionIdList)
                {
                    var actionInfo = this.CurrentDBSession.ActionInfoDal.LoadEntities(a => a.ID == actionId).FirstOrDefault();
                    roleInfo.ActionInfo.Add(actionInfo);
                }
                return this.CurrentDBSession.SaveChanges();
            }
            return false;
        }


        /// <summary>
        /// 批量删除多条角色对象数据
        /// </summary>
        /// <param name="list">角色信息列表ID集合</param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var roleInfoList = this.CurrentDBSession.RoleInfoDal.LoadEntities(u => list.Contains(u.ID));
            foreach (var roleInfo in roleInfoList)
            {
                this.CurrentDBSession.RoleInfoDal.DeleteEntity(roleInfo);
            }
            return this.CurrentDBSession.SaveChanges();
        }
    }
}
