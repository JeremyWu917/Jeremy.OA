using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.Common
{
    /// <summary>
    /// 封装了MemCache常用的方法
    /// </summary>
    public class MemcacheHelper
    {
        private static readonly MemcachedClient mc = null;

        /// <summary>
        /// 构造函数，默认连接127.0.0.1:11211
        /// </summary>
        static MemcacheHelper()
        {
            //最好放在配置文件中
            string[] serverlist = { "127.0.0.1:11211", "10.0.0.132:11211" };

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(serverlist);

            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;

            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;

            pool.MaintenanceSleep = 30;
            pool.Failover = true;

            pool.Nagle = false;
            pool.Initialize();

            // 获得客户端实例
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }
        
        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool Set(string key, object value)
        {
            return mc.Set(key, value);
        }

        /// <summary>
        /// 存储数据并指定过期时间
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="time">过期时间，注意默认最大30天</param>
        /// <returns></returns>
        public static bool Set(string key, object value, DateTime time)
        {
            return mc.Set(key, value, time);
        }

        /// <summary>
        /// 根据键获取数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return mc.Get(key);
        }

        /// <summary>
        /// 根据键删除数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static bool Delete(string key)
        {
            if (mc.KeyExists(key))
            {
                return mc.Delete(key);

            }
            return false;
        }
    
    }
}
