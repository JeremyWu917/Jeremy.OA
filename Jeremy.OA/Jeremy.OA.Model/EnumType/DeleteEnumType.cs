using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.Model.EnumType
{
    public enum DeleteEnumType
    {
        /// <summary>
        /// 正常的
        /// </summary>
        Normal=0,

        /// <summary>
        /// 逻辑删除、软删除
        /// </summary>
        LogicDelete = 1
    }
}
