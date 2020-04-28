using Jeremy.OA.Client.Models;
using log4net;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Jeremy.OA.Client
{
    public class MvcApplication : SpringMvcApplication//System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 获取log4net配置信息
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // 开启一个线程，扫描异常信息队列
            string filePath = Server.MapPath("/Log/");
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    //判断队列中有没有数据
                    if (MyExceptionAttribute.ExceptionsQueue.Count() > 0)
                    {
                        Exception ex = MyExceptionAttribute.ExceptionsQueue.Dequeue();
                        if (ex != null)
                        {
                            // 将异常信息写道日志中
                            //string fileName = "Logs" + DateTime.Now.ToString("yyyy-MM-dd");
                            //string msg = DateTime.Now.ToString() + Environment.NewLine + ex.ToString() + Environment.NewLine;
                            //File.AppendAllText(filePath + fileName + ".txt", msg, System.Text.Encoding.UTF8);
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex.ToString());
                        }
                        else
                        {
                            Thread.Sleep(3000);//等一会
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);//等一会
                    }
                }

            }, filePath);


        }
    }
}
