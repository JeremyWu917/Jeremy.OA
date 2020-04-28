using Jeremy.OA.Client.Models;
using System.Web;
using System.Web.Mvc;

namespace Jeremy.OA.Client
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            // 注册异常处理
            filters.Add(new MyExceptionAttribute());
        }
    }
}
