using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnowledgeBase.Add.IBLL;
using KnowledgeBase.Add.Model;
using KnowledgeBase.Add.BLL;

namespace KnowledgeBase.Add.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
       
        public ITestService testService { get; set; }

        public IUserInfoService userInfoService { get; set; }
        public ActionResult Index()
        {
            var testList = testService.LoadEntities(u => true);
            var testList1 = userInfoService.LoadEntities(u => true);
            ViewData.Model = testList;
            ViewBag.test = "测试";
            return View();
        }
        [ValidateInput(false)]
        public ActionResult Add(FormCollection fc)
        {
            Test testModel = new Test();
            string  i=Guid.NewGuid().ToString();

            testModel.Id = Guid.NewGuid().ToString();
            testModel.Name = "王杰"+i;
            testModel.Password = "123456" + i;
            testModel.Type = "News"+i;
            testModel.Ueditor = fc["editor"];

            if (testService.AddEntity(testModel))
            {
                return JavaScript("alert('ok');");
            }
            else
            {
                return JavaScript("alert('error');");
            }
        }
    }
}
