using Jeremy.OA.Model;
using Jeremy.OA.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.IBLL
{
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        /// <summary>
        /// 批量删除用户对象
        /// </summary>
        /// <param name="list">用户信息列表ID集合</param>
        /// <returns></returns>
        bool DeleteEntities(List<int> list);
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="userInfoSearch"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        IQueryable<UserInfo> LoadSearchEntites(UserInfoSearch userInfoSearch, int delFlag);
        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIdList"></param>
        /// <returns></returns>
        bool SetUserRoleInfo(int userId, List<int> roleIdList);
        /// <summary>
        /// 为用户分配权限
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="userId"></param>
        /// <param name="isPass"></param>
        /// <returns></returns>
        bool SetUserActionInfo(int actionId, int userId, bool isPass);
    }
}
