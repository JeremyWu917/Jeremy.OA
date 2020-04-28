using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.Model.Search
{
    public class UserInfoSearch : BaseSearch
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string UserRemark { get; set; }
    }
}
