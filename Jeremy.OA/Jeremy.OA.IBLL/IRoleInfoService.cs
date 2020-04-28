using Jeremy.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.IBLL
{
    public partial interface IRoleInfoService : IBaseService<RoleInfo>
    {
        // bool SetRoleActionInfo(int roleId, List<int> actionIdList);
        /// <summary>
        /// 批量删除用户对象
        /// </summary>
        /// <param name="list">用户信息列表ID集合</param>
        /// <returns></returns>
        bool DeleteEntities(List<int> list);
    }
}
