using Jeremy.OA.IDAL;
using Jeremy.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.DAL
{
    /// <summary>
    /// 先继承，再重载
    /// </summary>
    public partial class UserInfoDal : BaseDal<UserInfo>, IUserInfoDal
    {

    }
}
