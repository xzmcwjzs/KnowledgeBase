using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KnowledgeBase.Add.IBLL;
using KnowledgeBase.Add.Model;
using KnowledgeBase.Add.BLL;
using KnowledgeBase.Add.Common;


namespace KnowledgeBase.Select.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Login/

        public IUserInfoSelectService userInfoSelectService = new UserInfoSelectService();
        public ActionResult Index()
        {
            return View();
        }


        #region 显示验证码
        public ActionResult ShowValidateCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;

            byte[] buffer = validateCode.CreateValidateGraphic(code);//将验证吗画到画布上
            return File(buffer, "image/jpeg");
        }
        #endregion

        #region 登录
        public ActionResult DengLu()
        {
            string validateCode = Session["validateCode"] != null ? Session["validateCode"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("验证码输入错误");
            }
            //Session["validateCode"] = null;
            string txtCode = Request["txtCode"];
            if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("验证码输入错误");
            }
            string txtUserName = Request["txtUserName"];
            string txtPassword = Request["txtPassword"];
            var userInfoSelect = userInfoSelectService.LoadEntities(u => u.Name == txtUserName && u.PassWord == txtPassword).FirstOrDefault();

            if (userInfoSelect != null)
            {
                Session["userInfoSelect"] = userInfoSelect;
               return Content("ok");
            }
            else
            {
                return Content("用户名或密码错误");
            }
        }
        #endregion

        #region 测试页面
        public ActionResult Test()
        {
            return View();
        } 
        #endregion

        public ActionResult MobileIndex()
        {
            string name = CommonFunc.FilterSpecialString((CommonFunc.SafeGetStringFromObj(Request.Form["Name"])));
            string password = CommonFunc.FilterSpecialString((CommonFunc.SafeGetStringFromObj(Request.Form["Password"])));
            var mobileUserInfo =
                userInfoSelectService.LoadEntities(u => u.Name == name && u.PassWord == password).FirstOrDefault();
            if (mobileUserInfo != null)
            {
                Session["userInfoSelect"] = mobileUserInfo;
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
        }

    }
}
