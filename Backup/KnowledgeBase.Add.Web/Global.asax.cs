using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using KnowledgeBase.Add.Web.Common;

namespace KnowledgeBase.Add.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication :System.Web.HttpApplication //Spring.Web.Mvc.SpringMvcApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string fileLogPath = Server.MapPath("/Common/Log/");
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count > 0)
                    {
                        Exception ex = MyExceptionAttribute.ExceptionQueue.Dequeue();
                        string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(fileLogPath + fileName, ex.ToString(), System.Text.Encoding.Default);
                    }
                    else
                    {
                        Thread.Sleep(3000);//如果队列中没有数据，休息避免造成CPU的空转
                    }
                }

            }, fileLogPath);

        }
    }
}