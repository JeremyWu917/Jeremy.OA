using Jeremy.OA.DAL;
using Jeremy.OA.IDAL;
using Jeremy.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.DALFactory
{
    /// <summary>
    /// 数据会话层类  ->  工厂类
    /// 1、负责完成所有数据操作类实例的创建
    /// 2、业务层通过数据会话层来获取要操作数据类的实例
    /// 3、将业务层和数据层解耦
    /// 4、完成所有数据的保存
    /// </summary>
    public partial class DBSession : IDBSession
    {
        //OAEntities Db = new OAEntities();

        public DbContext Db
        {
            get
            {
                return DBContextFactory.CreateDbContext();
            }
        }

        //private IUserInfoDal _UserInfoDal;
        //public IUserInfoDal UserInfoDal
        //{
        //    get
        //    {
        //        if (_UserInfoDal == null)
        //        {
        //            //_UserInfoDal = new UserInfoDal();
        //            // 通过抽象工厂封装了类的实例的创建
        //            _UserInfoDal = AbstractFactory.CreateUserInfoDal();
        //        }
        //        return _UserInfoDal;
        //    }
        //    set { _UserInfoDal = value; }
        //}

        /// <summary>
        /// 全部提交修改到数据库中
        /// 一个业务中经常涉及对多张表的操作，通过此过程实现一次连接（数据库），全部提交的功能
        /// 提高性能
        /// 工作单元模式
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }



    }
}
