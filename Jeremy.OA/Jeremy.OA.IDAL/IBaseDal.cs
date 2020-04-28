using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.IDAL
{
    /// <summary>
    /// 通用的数据层方法接口：CURD、分页查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IBaseDal<T> where T : class, new()
    {
        /// <summary>
        /// 通用实体类的查询接口
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 通用实体类的分页查询接口
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex">第几页索引</param>
        /// <param name="pageSize">每页几条记录</param>
        /// <param name="totalCount">总页数</param>
        /// <param name="orderbyLambda">条件</param>
        /// <param name="isAsc">升序或者降序</param>
        /// <returns></returns>
        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount,
System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 通用实体类的删除接口
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool DeleteEntity(T entity);

        /// <summary>
        /// 通用实体类的编辑、更新接口
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool EditEntity(T entity);

        /// <summary>
        /// 通用实体类的添加接口
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        T AddEntity(T entity);

    }

}
