using Jeremy.OA.DALFactory;
using Jeremy.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.BLL
{
    /// <summary>
    /// 基类业务逻辑层，封装了CURD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> where T : class, new()
    {
        public IDBSession CurrentDBSession
        {
            get
            {
                //return new DBSession();
                return DBSessionFactory.CreateDBSession();
            }
        }
        public IBaseDal<T> CurrentDal { get; set; }

        /// <summary>
        /// 抽象方法，子类去实现
        /// </summary>
        public abstract void SetCurrentDal();

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseService()
        {
            SetCurrentDal();//放在子类中去实现抽象方法
        }

        /// <summary>
        /// 添加实体类
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
            CurrentDBSession.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 删除实体类
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            return CurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 更新实体类
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
            return CurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 查询实体类
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.LoadEntities(whereLambda);
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
            return CurrentDal.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLamda,orderbyLambda, isAsc);
        }



    }
}
