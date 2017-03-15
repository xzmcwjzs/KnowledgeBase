using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KnowledgeBase.Add.Web.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["loginInfo"] == null)
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                //filterContext.Result = Redirect("/Login/Index");
                //filterContext.HttpContext.Response.Write("<script type='text/javascript'>"+
                //"if(window.top!=window.self){window.close(); window.top.location.href='/Login/Index'}else{window.location.href='/Login/Index'};</script>");

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index"
                }));


            }
        }

    }
}
