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
            // ��ȡlog4net������Ϣ
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // ����һ���̣߳�ɨ���쳣��Ϣ����
            string filePath = Server.MapPath("/Log/");
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    //�ж϶�������û������
                    if (MyExceptionAttribute.ExceptionsQueue.Count() > 0)
                    {
                        Exception ex = MyExceptionAttribute.ExceptionsQueue.Dequeue();
                        if (ex != null)
                        {
                            // ���쳣��Ϣд����־��
                            //string fileName = "Logs" + DateTime.Now.ToString("yyyy-MM-dd");
                            //string msg = DateTime.Now.ToString() + Environment.NewLine + ex.ToString() + Environment.NewLine;
                            //File.AppendAllText(filePath + fileName + ".txt", msg, System.Text.Encoding.UTF8);
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex.ToString());
                        }
                        else
                        {
                            Thread.Sleep(3000);//��һ��
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);//��һ��
                    }
                }

            }, filePath);


        }
    }
}
