using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeBase.Add.Web.Common
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>();
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            ExceptionQueue.Enqueue(filterContext.Exception);//将异常信息添加到队列中
            filterContext.HttpContext.Response.Redirect("/Common/Error.html");

        }
    }
}