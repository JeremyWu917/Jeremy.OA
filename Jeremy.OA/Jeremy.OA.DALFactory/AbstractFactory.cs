using Jeremy.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.DALFactory
{
    /// <summary>
    /// 通过反射的形式创建类的实例
    /// </summary>
    public partial class AbstractFactory
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
        private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];

        /// <summary>
        /// 创建UserInfo接口
        /// </summary>
        /// <returns></returns>
        //public static IUserInfoDal CreateUserInfoDal()
        //{
        //    string fullClassName = NameSpace + ".UserInfoDal";
        //    return CreateInstance(fullClassName) as IUserInfoDal;
        //}

        /// <summary>
        /// 私有方法：根据对象名称创建对象的实例
        /// </summary>
        /// <param name="fullClassName">对象全名称</param>
        /// <returns></returns>
        private static object CreateInstance(string fullClassName)
        {
            var assembly = Assembly.Load(AssemblyPath);
            return assembly.CreateInstance(fullClassName);
        }
    }
}
