using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnowledgeBase.Add.Common;
using KnowledgeBase.Add.IBLL;
using KnowledgeBase.Add.BLL;

namespace KnowledgeBase.Add.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public IUserInfoService userInfoService=new UserInfoService();
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
            var loginInfo = userInfoService.LoadEntities(u => u.Name == txtUserName && u.Password == txtPassword).FirstOrDefault();

            if (loginInfo != null)
            {
                if (!string.IsNullOrEmpty(loginInfo.Remark))
                {
                    Session["loginInfo"] = loginInfo;
                    return Content("ok");

                }
                else
                {
                    return Content("用户角色尚未分配，无法进行登录");
                }

            }
            else
            {
                return Content("用户名或密码错误");
            }
        }
        #endregion

    }
}
