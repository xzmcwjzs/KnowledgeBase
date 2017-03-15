using System.Web;
using System.Web.Mvc;
using KnowledgeBase.Add.Web.Common;

namespace KnowledgeBase.Add.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new MyExceptionAttribute());
        }
    }
}