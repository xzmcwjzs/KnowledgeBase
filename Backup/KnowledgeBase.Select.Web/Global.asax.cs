using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

namespace KnowledgeBase.Select.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("mobile")
            {
                ContextCondition = (context) => Regex.Match(context.GetOverriddenUserAgent(), @"android|iphone|ipad").Success
            });

            //DisplayModeProvider.Instance.Modes
            // .Insert(0, new DefaultDisplayMode("Android")
            // {
            //     ContextCondition = (context =>
            //           context.GetOverriddenUserAgent().IndexOf
            //           ("Android", StringComparison.OrdinalIgnoreCase) >= 0)
            // });
            //DisplayModeProvider.Instance.Modes
            // .Insert(1, new DefaultDisplayMode("FF")
            // {
            //     ContextCondition = (context =>
            //           context.GetOverriddenUserAgent().IndexOf
            //           ("Firefox", StringComparison.OrdinalIgnoreCase) >= 0)
            // });

            //DisplayModeProvider.Instance.Modes
            //.Insert(2, new DefaultDisplayMode("Chrome")
            //{
            //    ContextCondition = (context =>
            //          context.GetOverriddenUserAgent().IndexOf
            //          ("Chrome", StringComparison.OrdinalIgnoreCase) >= 0)
            //});

        }
    }
}