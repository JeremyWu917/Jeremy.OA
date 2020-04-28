using Jeremy.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.DAL
{
    public class BaseDal<T> where T : class, new()
    {
        /// <summary>
        /// 创建一个EF实体对象
        /// </summary>
        //OAEntities Db = new OAEntities();


        DbContext Db = DBContextFactory.CreateDbContext();

        /// <summary>
        /// 添加实体类
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            Db.Set<T>().Add(entity);
            //Db.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 删除实体类
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            //return Db.SaveChanges() > 0;
            return true;
        }

        /// <summary>
        /// 更新实体类
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            //return Db.SaveChanges() > 0;
            return true;
        }

        /// <summary>
        /// 查询实体类
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where<T>(whereLambda);
        }

        /// <summary>
        /// 分页查询实体类
        /// </summary>
        /// <typeparam name="s">实体类型</typeparam>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="totalCount">总页数</param>
        /// <param name="whereLamda">查询条件</param>
        /// <param name="orderbyLambda">查询方式</param>
        /// <param name="isAsc">true-升序 false-降序</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLamda, Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var temp = Db.Set<T>().Where<T>(whereLamda);
            totalCount = temp.Count();
            if (isAsc) //升序
            {
                temp = temp.OrderBy<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }

    }
}
