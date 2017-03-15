using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnowledgeBase.Add.IBLL;
using KnowledgeBase.Add.Model;
using KnowledgeBase.Add.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data;
using System.Data.Entity.Infrastructure;
using KnowledgeBase.Add.BLL;
using KnowledgeBase.Add.DAL;
using KnowledgeBase.Add.Model.ViewModel;

namespace KnowledgeBase.Add.Web.Controllers
{
    public class FrameController : BaseController
    {
        //
        // GET: /Frame/
        public IUserInfoService userInfoService =new UserInfoService();
        public IDiseaseInfoService diseaseInfoService =new DiseaseInfoService();
        public IModulesContentsService modulesContentsService =new ModulesContentsService();
        public IDiseaseCodeNameService diseaseCodeNameService =new DiseaseCodeNameService();
        public ActionResult Index()
        {
            UserInfo userInfoModel = (UserInfo)Session["loginInfo"];
            ViewBag.UserInfo = userInfoModel;
            return View();
        }
        #region 接收密码修改时参数
        [HttpGet]
        public ActionResult ModifyPassword()
        {
            TempData["name"] = CommonFunc.SafeGetStringFromObj(Request.QueryString["name"]);
            TempData["id"] = CommonFunc.SafeGetStringFromObj(Request.QueryString["identity"]);
            return View();
        } 
        #endregion

        #region 密码修改
        [HttpPost]
        public ActionResult ModifyPassword(FormCollection form)
        {
            UserInfo userInfoModel = new UserInfo();
            string name = CommonFunc.SafeGetStringFromObj(TempData["name"]);
            string id = CommonFunc.SafeGetStringFromObj(TempData["id"]);
            string txtPwd = CommonFunc.SafeGetStringFromObj(form["txtPwd"]);
            string txtConfirmPwd = CommonFunc.SafeGetStringFromObj(form["txtConfirmPwd"]);
            if (txtPwd.Equals(txtConfirmPwd))
            {
                userInfoModel.Id = id;
                userInfoModel.Name = name;
                userInfoModel.Password = txtPwd;

                if (userInfoService.UpdatePwd(userInfoModel))
                {
                    return Content("0");
                }
                else
                {
                    return Content("1");
                }
            }
            else
            {
                return Content("2");
            }
        } 
        #endregion

        #region MainFrame页
        public ActionResult MainFrame()
        {
            return View();
        } 
        #endregion

        #region top页
        public ActionResult Top()
        {
            UserInfo userInfoModel = (UserInfo)Session["loginInfo"];
            ViewBag.Remark = CommonFunc.SafeGetStringFromObj(userInfoModel.Remark);
            return View();
        } 
        #endregion

        #region body页
        public ActionResult Body()
        {
            return View();
        } 
        #endregion

        #region left页
        public ActionResult Left()
        {
            return View();
        } 
        #endregion

        #region List页
        public ActionResult List()
        {
            return View();
        } 
        #endregion

        #region 新增页
        public ActionResult Manage()
        {
            UserInfo userInfoModel = (UserInfo)Session["loginInfo"];
            ViewBag.RealName =CommonFunc.SafeGetStringFromObj(userInfoModel.RealName);
            ViewBag.Name = CommonFunc.SafeGetStringFromObj(userInfoModel.Name);
            ViewBag.Remark = CommonFunc.SafeGetStringFromObj(userInfoModel.Remark);
            return View();
        } 
        #endregion

        #region 新增疾病
        [ValidateInput(false)]
        public ActionResult AddOrUpdate()
        {
            DbContext Db = DbContextFactory.CreateDbContext();

            string modulesId = CommonFunc.SafeGetStringFromObj(Request["id"]);
            string diseaseCode = CommonFunc.SafeGetStringFromObj(Request["diseaseCode"]);
            string luRu = CommonFunc.SafeGetStringFromObj(Request["luRu"]);
            string luRuId = CommonFunc.SafeGetStringFromObj(Request["luRuId"]);
            string diseaseNameList = CommonFunc.SafeGetStringFromObj(Request["diseaseNameList"]);
            string spellList = CommonFunc.SafeGetStringFromObj(Request["spellList"]);
            string ywmcList = CommonFunc.SafeGetStringFromObj(Request["ywmcList"]);
            string ywsxList = CommonFunc.SafeGetStringFromObj(Request["ywsxList"]);
            string ueContent = Request["ueContent"];

            if (string.IsNullOrEmpty(diseaseCode))
            {
                return Json(new { status = "error", msg = "疾病分类号不能为空" });
            }
            if (string.IsNullOrEmpty(luRu))
            {
                return Json(new { status = "error", msg = "录入人不能为空" });
            }
            if (string.IsNullOrEmpty(diseaseNameList))
            {
                return Json(new { status = "error", msg = "疾病名称1不能为空" });
            }
            if (string.IsNullOrEmpty(spellList))
            {
                return Json(new { status = "error", msg = "疾病名称拼音首字母1不能为空" });
            }
            if (string.IsNullOrEmpty(ywmcList))
            {
                return Json(new { status = "error", msg = "疾病英文全称不能为空" });
            }
            DiseaseInfo diseaseInfoModel = new DiseaseInfo();
            ModulesContents modulesContentsModel = new ModulesContents();
            diseaseInfoModel.DiseaseCode = diseaseCode;
            diseaseInfoModel.DiseaseName = diseaseNameList;
            diseaseInfoModel.Spell = spellList;
            diseaseInfoModel.YWMC = ywmcList;
            diseaseInfoModel.YWSX = ywsxList;
            diseaseInfoModel.CreateTime = DateTime.Now.Date.ToString("yyyy-MM-dd");
            diseaseInfoModel.LuRu = luRu;
            diseaseInfoModel.LuRuId = luRuId;
            diseaseInfoModel.JDStatus = "0";
            diseaseInfoModel.Status = "0";
            Db.Set<DiseaseInfo>().AddOrUpdate(diseaseInfoModel);

            modulesContentsModel.Id = Guid.NewGuid().ToString();
            modulesContentsModel.DiseaseCode = diseaseInfoModel.DiseaseCode;
            modulesContentsModel.ModulesId = modulesId;
            modulesContentsModel.ModulesContent = ueContent;
            modulesContentsModel.CreateTime = DateTime.Now.Date.ToString("yyyy-MM-dd");


            int modulesContentsCount1 = Db.Set<ModulesContents>().Count();

            if (modulesContentsCount1 == 0)
            {
                Db.Set<ModulesContents>().Add(modulesContentsModel);
                int n = Db.SaveChanges();
                if (n > 0)
                {
                    return Json(new { status = "ok", msg = "提交成功" });
                }
                else
                {
                    return Json(new { status = "error", msg = "提交失败" });
                }
            }
            else
            {
                var list = Db.Set<ModulesContents>();
                int modulesContentsCount2 = list.Where(u => (u.DiseaseCode == diseaseCode)
                    && (u.ModulesId == modulesId)).Count();
                if (modulesContentsCount2 != 0)
                {
                    //AsNoTracking作用：
                    var modulesContentsList = list.AsNoTracking().Where(u => (u.DiseaseCode == diseaseCode)
                    && (u.ModulesId == modulesId)).FirstOrDefault();

                    ModulesContents modulesContentsModel2 = new ModulesContents();
                    modulesContentsModel2.Id = modulesContentsList.Id;
                    modulesContentsModel2.DiseaseCode = diseaseCode;
                    modulesContentsModel2.ModulesId = modulesId;
                    modulesContentsModel2.ModulesContent = ueContent;
                    modulesContentsModel2.CreateTime = DateTime.Now.Date.ToString("yyyy-MM-dd");

                    Db.Set<ModulesContents>().Attach(modulesContentsModel2);
                    Db.Entry<ModulesContents>(modulesContentsModel2).State = EntityState.Modified;
                    int n = Db.SaveChanges();
                    if (n > 0)
                    {
                        return Json(new { status = "ok", msg = "更新成功" });
                    }
                    else
                    {
                        return Json(new { status = "error", msg = "更新失败" });
                    }
                }
                else
                {
                    Db.Set<ModulesContents>().Add(modulesContentsModel);
                    int n = Db.SaveChanges();
                    if (n > 0)
                    {
                        return Json(new { status = "ok", msg = "提交成功" });
                    }
                    else
                    {
                        return Json(new { status = "error", msg = "提交失败" });
                    }
                }
            }

        }
        #endregion

        #region 加载分页列表

        public ActionResult LeftToList(string LuRu, string DiseaseName, string Spell, string YWMC, string YWSX, string JDStatus, string Status)
        {
            ViewData["LuRu"] = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(LuRu));
            ViewData["DiseaseName"] = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(DiseaseName));
            ViewData["Spell"] = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(Spell));
            ViewData["YWMC"] = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(YWMC));
            ViewData["YWSX"] = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(YWSX));
            ViewData["JDStatus"] = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(JDStatus));
            ViewData["Status"] = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(Status));
            return View("List");
        }
        //[ValidateInput(false)]
        public ActionResult PageList()
        {
            UserInfo userInfoModel = (UserInfo)Session["loginInfo"];
            string remark = userInfoModel.Remark;
            string realName = userInfoModel.RealName;
            string name = userInfoModel.Name;
            int pageIndex = Request["pi"] != null ? int.Parse(Request["pi"]) : 1;
            int pageSize = 15;
            string LuRu = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(Request["LuRu"]));
            string DiseaseName = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(Request["DiseaseName"]));
            string Spell = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(Request["Spell"]));
            string YWMC = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(Request["YWMC"]));
            string YWSX = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(Request["YWSX"]));
            string JDStatus = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(Request["JDStatus"]));
            string Status = CommonFunc.FilterSpecialString(CommonFunc.SafeGetStringFromObj(Request["Status"]));

            int totalCount = 0;

            ExpressionHelper<DiseaseInfo> expression = new ExpressionHelper<DiseaseInfo>();
            if (LuRu != "")
            {
                expression.Contains("LuRu", LuRu);
            }
            if (DiseaseName != "")
            {
                expression.Contains("DiseaseName", DiseaseName);
            }
            if (Spell != "")
            {
                expression.Contains("Spell", Spell);
            }
            if (YWMC != "")
            {
                expression.Contains("YWMC", YWMC);
            }
            if (YWSX != "")
            {
                expression.Equal("YWSX", YWSX);
            }
            if (JDStatus != "")
            {
                expression.Equal("JDStatus", JDStatus);
            }
            if (Status != "")
            {
                expression.Equal("Status", Status);
            }
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            var diseaseInfoList = diseaseInfoService.LoadPageEntities<string>(pageSize, pageIndex, out totalCount, expression.GetExpression(), u => u.CreateTime, false);
            var myDiseaseInfo = from u in diseaseInfoList
                select new
                {
                    diseaseCode = u.DiseaseCode,
                    diseaseName=u.DiseaseName,
                    spell=u.Spell,
                    ywmc=u.YWMC,
                    ywsx=u.YWSX,
                    createTime=u.CreateTime,
                    luRu=u.LuRu==null?"":u.LuRu,
                    jiaoDui=u.JiaoDui==null?"":u.JiaoDui,
                    shenHe=u.ShenHe==null?"":u.ShenHe,
                    luRuId = u.LuRuId == null ? "" : u.LuRuId,
                    jiaoDuiId = u.JiaoDuiId == null ? "" : u.JiaoDuiId,
                    shenHeId = u.ShenHeId == null ? "" : u.ShenHeId,
                    jdStatus=u.JDStatus,
                    status=u.Status
                };
            int PageCount = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

            return Json(new { data = myDiseaseInfo, totalCount, pageCount = PageCount, remark ,realName,name});

        }
        #endregion

        #region 查看
        public ActionResult Show()
        {
            string diseaseCode = CommonFunc.SafeGetStringFromObj(Request.QueryString["diseaseCode"]);
            var modulesContentsSet = modulesContentsService.LoadEntities(u => u.DiseaseCode == diseaseCode);
            var myModulesContentsSet = from modulesContents in modulesContentsSet
                select new DiseaseContentsViewModel
                {
                    VDiseaseCode = modulesContents.DiseaseInfo.DiseaseCode,
                    VDiseaseName = modulesContents.DiseaseInfo.DiseaseName,
                    VSpell = modulesContents.DiseaseInfo.Spell,
                    VYWMC = modulesContents.DiseaseInfo.YWMC,
                    VYWSX = modulesContents.DiseaseInfo.YWSX,
                    VCreateTime = modulesContents.DiseaseInfo.CreateTime,
                    VLuRu = modulesContents.DiseaseInfo.LuRu,
                    VJiaoDui = modulesContents.DiseaseInfo.JiaoDui,
                    VShenHe = modulesContents.DiseaseInfo.ShenHe,
                    VStatus = modulesContents.DiseaseInfo.Status,
                    VJDStatus = modulesContents.DiseaseInfo.JDStatus,
                    VId=modulesContents.Id,
                    VModulesId = modulesContents.ModulesId,
                    VModulesContent = modulesContents.ModulesContent
                };
            ViewData.Model = myModulesContentsSet;
            return View();
        } 
        #endregion

        #region 修改
        public ActionResult XiuGai()
        {
            string DiseaseCode = CommonFunc.SafeGetStringFromObj(Request.QueryString["diseaseCode"]);
            string pi = CommonFunc.SafeGetStringFromObj(Request.QueryString["pi"]); 
            var modulesContentsSet = modulesContentsService.LoadEntities(u => u.DiseaseCode == DiseaseCode);
            var myModulesContentsSet = from modulesContents in modulesContentsSet
                                       select new DiseaseContentsViewModel
                                       {
                                           VDiseaseCode = modulesContents.DiseaseInfo.DiseaseCode,
                                           VDiseaseName = modulesContents.DiseaseInfo.DiseaseName,
                                           VSpell = modulesContents.DiseaseInfo.Spell,
                                           VYWMC = modulesContents.DiseaseInfo.YWMC,
                                           VYWSX = modulesContents.DiseaseInfo.YWSX,
                                           VCreateTime = modulesContents.DiseaseInfo.CreateTime,
                                           VLuRu = modulesContents.DiseaseInfo.LuRu,  
                                           VJiaoDui = modulesContents.DiseaseInfo.JiaoDui,
                                           VShenHe = modulesContents.DiseaseInfo.ShenHe,
                                           VStatus = modulesContents.DiseaseInfo.Status,
                                           VJDStatus = modulesContents.DiseaseInfo.JDStatus,
                                           VId = modulesContents.Id,
                                           VModulesId = modulesContents.ModulesId,
                                           VModulesContent = modulesContents.ModulesContent
                                       };
            ViewData.Model = myModulesContentsSet;
            ViewBag.pi = pi;
            return View();
        }
        #endregion

        #region 修改疾病
        [ValidateInput(false)]
        public ActionResult Update()
        { 
            DbContext Db = DbContextFactory.CreateDbContext();
            string diseaseCode = CommonFunc.SafeGetStringFromObj(Request["diseaseCode"]);  
            string diseaseNameList = CommonFunc.SafeGetStringFromObj(Request["diseaseNameList"]);
            string spellList = CommonFunc.SafeGetStringFromObj(Request["spellList"]);
            string ywmcList = CommonFunc.SafeGetStringFromObj(Request["ywmcList"]);
            string ywsxList = CommonFunc.SafeGetStringFromObj(Request["ywsxList"]);
            string id = CommonFunc.SafeGetStringFromObj(Request["nid"]);
            string modulesId = CommonFunc.SafeGetStringFromObj(Request["modulesId"]);
            string ueContent = Request["ueContent"];
 
            if (string.IsNullOrEmpty(diseaseNameList))
            {
                return Json(new { status = "error", msg = "疾病名称不能为空" });
            }
            if (string.IsNullOrEmpty(spellList))
            {
                return Json(new { status = "error", msg = "疾病名称拼音首字母不能为空" });
            }
            if (string.IsNullOrEmpty(ywmcList))
            {
                return Json(new { status = "error", msg = "疾病英文全称不能为空" });
            }
            DiseaseInfo diseaseInfoModel = new DiseaseInfo();
            ModulesContents modulesContentsModel = new ModulesContents();
            diseaseInfoModel.DiseaseCode = diseaseCode;
            diseaseInfoModel.DiseaseName = diseaseNameList;
            diseaseInfoModel.Spell = spellList;
            diseaseInfoModel.YWMC = ywmcList;
            diseaseInfoModel.YWSX = ywsxList;
            diseaseInfoModel.CreateTime = DateTime.Now.Date.ToString("yyyy-MM-dd");

            Db.Set<DiseaseInfo>().Add(diseaseInfoModel);
            DbEntityEntry<DiseaseInfo> entry = Db.Entry<DiseaseInfo>(diseaseInfoModel);
            entry.State = EntityState.Unchanged;
            entry.Property(u => u.DiseaseName).IsModified = true;
            entry.Property(u => u.Spell).IsModified = true;
            entry.Property(u => u.YWMC).IsModified = true;
            entry.Property(u => u.YWSX).IsModified = true;
            entry.Property(u => u.CreateTime).IsModified = true;

            //modulesContentsModel.Id = id;
            modulesContentsModel.DiseaseCode = diseaseCode;
            modulesContentsModel.ModulesId = modulesId;
            modulesContentsModel.ModulesContent = ueContent;
            modulesContentsModel.CreateTime = DateTime.Now.Date.ToString("yyyy-MM-dd");
            if (Db.Set<ModulesContents>().AsNoTracking().Where(u => (u.DiseaseCode == diseaseCode) && (u.ModulesId == modulesId)).Count() > 0)
            {
                modulesContentsModel.Id = id;
                Db.Set<ModulesContents>().Attach(modulesContentsModel);
                Db.Entry<ModulesContents>(modulesContentsModel).State = EntityState.Modified;
            }
            else
            {
                modulesContentsModel.Id = Guid.NewGuid().ToString();
                Db.Set<ModulesContents>().Add(modulesContentsModel);
            }
            int n= Db.SaveChanges();
            if (n > 0)
            {
                return Json(new { status = "ok", msg = "疾病内容修改成功"});
            }
            else
            {
                return Json(new { status = "error", msg = "疾病内容修改失败" });
            }

        }
        #endregion

        #region 校对
        public ActionResult JiaoDui()
        {
            UserInfo userInfoModel =(UserInfo)Session["loginInfo"];
            ViewBag.Name = CommonFunc.SafeGetStringFromObj(userInfoModel.Name);
            ViewBag.RealName = CommonFunc.SafeGetStringFromObj(userInfoModel.RealName); 
            string DiseaseCode = CommonFunc.SafeGetStringFromObj(Request.QueryString["diseaseCode"]);
            string pi = CommonFunc.SafeGetStringFromObj(Request.QueryString["pi"]); 
            var modulesContentsSet = modulesContentsService.LoadEntities(u => u.DiseaseCode == DiseaseCode);
            var myModulesContentsSet = from modulesContents in modulesContentsSet
                                       select new DiseaseContentsViewModel
                                       {
                                           VDiseaseCode = modulesContents.DiseaseInfo.DiseaseCode,
                                           VDiseaseName = modulesContents.DiseaseInfo.DiseaseName,
                                           VSpell = modulesContents.DiseaseInfo.Spell,
                                           VYWMC = modulesContents.DiseaseInfo.YWMC,
                                           VYWSX = modulesContents.DiseaseInfo.YWSX,
                                           VCreateTime = modulesContents.DiseaseInfo.CreateTime,
                                           VLuRu = modulesContents.DiseaseInfo.LuRu,
                                           VLuRuId = modulesContents.DiseaseInfo.LuRuId,
                                           VJiaoDui = modulesContents.DiseaseInfo.JiaoDui,
                                           VJiaoDuiId = modulesContents.DiseaseInfo.JiaoDuiId,
                                           VShenHe = modulesContents.DiseaseInfo.ShenHe,
                                           VShenHeId = modulesContents.DiseaseInfo.ShenHeId,
                                           VJDStatus = modulesContents.DiseaseInfo.JDStatus,
                                           VStatus = modulesContents.DiseaseInfo.Status,
                                           VId = modulesContents.Id,
                                           VModulesId = modulesContents.ModulesId,
                                           VModulesContent = modulesContents.ModulesContent
                                       };
            ViewData.Model = myModulesContentsSet;
            ViewBag.pi = pi;
            return View();
        }
        #endregion

        #region 校对疾病
        [ValidateInput(false)]
        public ActionResult UpdateJiaoDui()
        { 
            DbContext Db = DbContextFactory.CreateDbContext();
            string diseaseCode = CommonFunc.SafeGetStringFromObj(Request["diseaseCode"]);
            string diseaseNameList = CommonFunc.SafeGetStringFromObj(Request["diseaseNameList"]);
            string spellList = CommonFunc.SafeGetStringFromObj(Request["spellList"]);
            string ywmcList = CommonFunc.SafeGetStringFromObj(Request["ywmcList"]);
            string ywsxList = CommonFunc.SafeGetStringFromObj(Request["ywsxList"]);
            string id = CommonFunc.SafeGetStringFromObj(Request["nid"]);
            string modulesId = CommonFunc.SafeGetStringFromObj(Request["modulesId"]);
            string ueContent = Request["ueContent"];
            string jiaoDui = CommonFunc.SafeGetStringFromObj(Request["jiaoDui"]);
            string jiaoDuiId = CommonFunc.SafeGetStringFromObj(Request["jiaoDuiId"]);

            if (string.IsNullOrEmpty(diseaseNameList))
            {
                return Json(new { status = "error", msg = "疾病名称不能为空" });
            }
            if (string.IsNullOrEmpty(spellList))
            {
                return Json(new { status = "error", msg = "疾病名称拼音首字母不能为空" });
            }
            if (string.IsNullOrEmpty(ywmcList))
            {
                return Json(new { status = "error", msg = "疾病英文全称不能为空" });
            }
            DiseaseInfo diseaseInfoModel = new DiseaseInfo();
            ModulesContents modulesContentsModel = new ModulesContents();
            diseaseInfoModel.DiseaseCode = diseaseCode;
            diseaseInfoModel.DiseaseName = diseaseNameList;
            diseaseInfoModel.Spell = spellList;
            diseaseInfoModel.YWMC = ywmcList;
            diseaseInfoModel.YWSX = ywsxList;
            diseaseInfoModel.CreateTime = DateTime.Now.Date.ToString("yyyy-MM-dd");
            diseaseInfoModel.JiaoDui = jiaoDui;
            diseaseInfoModel.JiaoDuiId = jiaoDuiId;

            Db.Set<DiseaseInfo>().Add(diseaseInfoModel);
            DbEntityEntry<DiseaseInfo> entry = Db.Entry<DiseaseInfo>(diseaseInfoModel);
            entry.State = EntityState.Unchanged;
            entry.Property(u => u.DiseaseName).IsModified = true;
            entry.Property(u => u.Spell).IsModified = true;
            entry.Property(u => u.YWMC).IsModified = true;
            entry.Property(u => u.YWSX).IsModified = true;
            entry.Property(u => u.CreateTime).IsModified = true;
            entry.Property(u => u.JiaoDui).IsModified = true;
            entry.Property(u => u.JiaoDuiId).IsModified = true;

            //modulesContentsModel.Id = id;
            modulesContentsModel.DiseaseCode = diseaseCode;
            modulesContentsModel.ModulesId = modulesId;
            modulesContentsModel.ModulesContent = ueContent;
            modulesContentsModel.CreateTime = DateTime.Now.Date.ToString("yyyy-MM-dd");
            if (Db.Set<ModulesContents>().AsNoTracking().Where(u => (u.DiseaseCode == diseaseCode) && (u.ModulesId == modulesId)).Count() > 0)
            {
                modulesContentsModel.Id = id;
                Db.Set<ModulesContents>().Attach(modulesContentsModel);
                Db.Entry<ModulesContents>(modulesContentsModel).State = EntityState.Modified;
            }
            else
            {
                modulesContentsModel.Id = Guid.NewGuid().ToString();
                Db.Set<ModulesContents>().Add(modulesContentsModel);
            }
            int n = Db.SaveChanges();
            if (n > 0)
            {
                return Json(new { status = "ok", msg = "疾病内容修改成功" });
            }
            else
            {
                return Json(new { status = "error", msg = "疾病内容修改失败" });
            }

        }
        #endregion

        #region 提交校对
        public ActionResult TiJiaoJD()
        {
            string diseaseCode = CommonFunc.SafeGetStringFromObj(Request["diseaseCode"]);
            string jiaoDui = CommonFunc.SafeGetStringFromObj(Request["jiaoDui"]);
            string jiaoDuiId = CommonFunc.SafeGetStringFromObj(Request["jiaoDuiId"]);
            string jdStatus = "1";

            DiseaseInfo diseaseInfoModel = new DiseaseInfo();
            diseaseInfoModel.DiseaseCode = diseaseCode;
            diseaseInfoModel.JiaoDui = jiaoDui;
            diseaseInfoModel.JiaoDuiId = jiaoDuiId;
            diseaseInfoModel.JDStatus = jdStatus;

            if (diseaseInfoService.TiJiaoJD(diseaseInfoModel))
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
        }
        #endregion

        #region 审核
        public ActionResult ShenHe()
        {
            UserInfo userInfoModel = (UserInfo)Session["loginInfo"];
            ViewBag.Name = CommonFunc.SafeGetStringFromObj(userInfoModel.Name);
            ViewBag.RealName = CommonFunc.SafeGetStringFromObj(userInfoModel.RealName); 
            string DiseaseCode = CommonFunc.SafeGetStringFromObj(Request.QueryString["diseaseCode"]);
            string pi = CommonFunc.SafeGetStringFromObj(Request.QueryString["pi"]); 
            var modulesContentsSet = modulesContentsService.LoadEntities(u => u.DiseaseCode == DiseaseCode);
            var myModulesContentsSet = from modulesContents in modulesContentsSet
                                       select new DiseaseContentsViewModel
                                       {
                                           VDiseaseCode = modulesContents.DiseaseInfo.DiseaseCode,
                                           VDiseaseName = modulesContents.DiseaseInfo.DiseaseName,
                                           VSpell = modulesContents.DiseaseInfo.Spell,
                                           VYWMC = modulesContents.DiseaseInfo.YWMC,
                                           VYWSX = modulesContents.DiseaseInfo.YWSX,
                                           VCreateTime = modulesContents.DiseaseInfo.CreateTime,
                                           VLuRu = modulesContents.DiseaseInfo.LuRu,
                                           VJiaoDui = modulesContents.DiseaseInfo.JiaoDui,
                                           VShenHe = modulesContents.DiseaseInfo.ShenHe,
                                           VJDStatus = modulesContents.DiseaseInfo.JDStatus,
                                           VStatus = modulesContents.DiseaseInfo.Status,
                                           VId = modulesContents.Id,
                                           VModulesId = modulesContents.ModulesId,
                                           VModulesContent = modulesContents.ModulesContent
                                       };
            ViewData.Model = myModulesContentsSet;
            ViewBag.pi = pi;
            return View();
        }
        #endregion

        #region 审核疾病
        [ValidateInput(false)]
        public ActionResult UpdateShenHe()
        {
            DbContext Db = DbContextFactory.CreateDbContext();
            string diseaseCode = CommonFunc.SafeGetStringFromObj(Request["diseaseCode"]);
            string diseaseNameList = CommonFunc.SafeGetStringFromObj(Request["diseaseNameList"]);
            string spellList = CommonFunc.SafeGetStringFromObj(Request["spellList"]);
            string ywmcList = CommonFunc.SafeGetStringFromObj(Request["ywmcList"]);
            string ywsxList = CommonFunc.SafeGetStringFromObj(Request["ywsxList"]);
            string id = CommonFunc.SafeGetStringFromObj(Request["nid"]);
            string modulesId = CommonFunc.SafeGetStringFromObj(Request["modulesId"]);
            string ueContent = Request["ueContent"]; 

            if (string.IsNullOrEmpty(diseaseNameList))
            {
                return Json(new { status = "error", msg = "疾病名称不能为空" });
            }
            if (string.IsNullOrEmpty(spellList))
            {
                return Json(new { status = "error", msg = "疾病名称拼音首字母不能为空" });
            }
            if (string.IsNullOrEmpty(ywmcList))
            {
                return Json(new { status = "error", msg = "疾病英文全称不能为空" });
            }
            DiseaseInfo diseaseInfoModel = new DiseaseInfo();
            ModulesContents modulesContentsModel = new ModulesContents();
            diseaseInfoModel.DiseaseCode = diseaseCode;
            diseaseInfoModel.DiseaseName = diseaseNameList;
            diseaseInfoModel.Spell = spellList;
            diseaseInfoModel.YWMC = ywmcList;
            diseaseInfoModel.YWSX = ywsxList;
            diseaseInfoModel.CreateTime = DateTime.Now.Date.ToString("yyyy-MM-dd"); 

            Db.Set<DiseaseInfo>().Add(diseaseInfoModel);
            DbEntityEntry<DiseaseInfo> entry = Db.Entry<DiseaseInfo>(diseaseInfoModel);
            entry.State = EntityState.Unchanged;
            entry.Property(u => u.DiseaseName).IsModified = true;
            entry.Property(u => u.Spell).IsModified = true;
            entry.Property(u => u.YWMC).IsModified = true;
            entry.Property(u => u.YWSX).IsModified = true;
            entry.Property(u => u.CreateTime).IsModified = true; 

            //modulesContentsModel.Id = id;
            modulesContentsModel.DiseaseCode = diseaseCode;
            modulesContentsModel.ModulesId = modulesId;
            modulesContentsModel.ModulesContent = ueContent;
            modulesContentsModel.CreateTime = DateTime.Now.Date.ToString("yyyy-MM-dd");
            if (Db.Set<ModulesContents>().AsNoTracking().Where(u => (u.DiseaseCode == diseaseCode) && (u.ModulesId == modulesId)).Count() > 0)
            {
                modulesContentsModel.Id = id;
                Db.Set<ModulesContents>().Attach(modulesContentsModel);
                Db.Entry<ModulesContents>(modulesContentsModel).State = EntityState.Modified;
            }
            else
            {
                modulesContentsModel.Id = Guid.NewGuid().ToString();
                Db.Set<ModulesContents>().Add(modulesContentsModel);
            }
            int n = Db.SaveChanges();
            if (n > 0)
            {
                return Json(new { status = "ok", msg = "疾病内容修改成功" });
            }
            else
            {
                return Json(new { status = "error", msg = "疾病内容修改失败" });
            }

        }
        #endregion

        #region 提交审核
        public ActionResult TiJiaoShenHe()
        {
            string diseaseCode = CommonFunc.SafeGetStringFromObj(Request["diseaseCode"]);
            string shenHe = CommonFunc.SafeGetStringFromObj(Request["shenHe"]);
            string shenHeId = CommonFunc.SafeGetStringFromObj(Request["shenHeId"]);
            string status = CommonFunc.SafeGetStringFromObj(Request["status"]);

            DiseaseInfo diseaseInfoModel = new DiseaseInfo();
            diseaseInfoModel.DiseaseCode = diseaseCode;
            diseaseInfoModel.ShenHe = shenHe;
            diseaseInfoModel.ShenHeId = shenHeId;
            diseaseInfoModel.Status = status;

            if (diseaseInfoService.TiJiaoSH(diseaseInfoModel))
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
        } 
        #endregion

        #region 自动加载疾病分类号
        public ActionResult AutocompleteDiseaseCode()
        {
            var diseaseCodeNameList = diseaseCodeNameService.LoadEntities(u=>true);
            return Json(diseaseCodeNameList,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 录入人本人提交通过

        public ActionResult Handle()
        {
            string  diseaseCode = CommonFunc.SafeGetStringFromObj(Request["DiseaseCode"]);
            string status = CommonFunc.SafeGetStringFromObj(Request["Status"]);
            if (status == "0")
            {
                status = "1";
            }
            else
            {
                status = "0";
            }

            DiseaseInfo diseaseInfoModel = new DiseaseInfo();
            diseaseInfoModel.DiseaseCode = diseaseCode; 
            diseaseInfoModel.Status = status;

            if (diseaseInfoService.LuRuSH(diseaseInfoModel))
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
        }
        #endregion
    }
}
