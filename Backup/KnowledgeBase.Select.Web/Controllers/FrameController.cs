using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnowledgeBase.Add.IBLL;
using KnowledgeBase.Add.BLL;
using KnowledgeBase.Add.Model;
using KnowledgeBase.Add.Model.ViewModel;
using KnowledgeBase.Add.Common;
using System.Data.Entity;
using KnowledgeBase.Add.DAL;

namespace KnowledgeBase.Select.Web.Controllers
{
    public class FrameController : BaseController
    {
        //
        // GET: /Frame/
        IDiseaseInfoService diseaseInfoService = new DiseaseInfoService();
        IModulesContentsService modulesContentsService = new ModulesContentsService();
        IModulesService modulesService = new ModulesService();
        public ActionResult Index()
        {
            UserInfoSelect userInfoSelectModel = (UserInfoSelect)Session["userInfoSelect"];
            ViewBag.IsPrint = userInfoSelectModel.IsPrint;
            return View();
        }
        #region 自动加载疾病
        public ActionResult AutocompleteDisease()
        {
            var diseaseInfoList = diseaseInfoService.LoadEntities(u=>u.Status=="1"); 
            List<DiseaseCodeNameViewModel> diseaseCodeNameViewModelList = new List<DiseaseCodeNameViewModel>();
            DiseaseCodeNameViewModel diseaseCodeNameViewModel; 
            foreach(var item in diseaseInfoList){  
                foreach (string s in item.DiseaseName.Split('}'))
                {
                    diseaseCodeNameViewModel = new DiseaseCodeNameViewModel();
                    if (s != "")
                    { 
                        diseaseCodeNameViewModel.VDiseaseCode = item.DiseaseCode;
                        diseaseCodeNameViewModel.VDiseaseList = s;
                        diseaseCodeNameViewModel.VDiseaseType = "疾病中文名称";
                        diseaseCodeNameViewModelList.Add(diseaseCodeNameViewModel);
                    }
                }
                foreach (string s in item.Spell.Split('}'))
                {
                    diseaseCodeNameViewModel = new DiseaseCodeNameViewModel();
                    if (s != "")
                    {
                        diseaseCodeNameViewModel.VDiseaseCode = item.DiseaseCode;
                        diseaseCodeNameViewModel.VDiseaseList = s;
                        diseaseCodeNameViewModel.VDiseaseType = "疾病名称拼音首字母";
                        diseaseCodeNameViewModelList.Add(diseaseCodeNameViewModel);
                    }
                }
                foreach (string s in item.YWMC.Split('}'))
                {
                    diseaseCodeNameViewModel = new DiseaseCodeNameViewModel();
                    if (s != "")
                    {
                        diseaseCodeNameViewModel.VDiseaseCode = item.DiseaseCode;
                        diseaseCodeNameViewModel.VDiseaseList = s;
                        diseaseCodeNameViewModel.VDiseaseType = "疾病英文名称";
                        diseaseCodeNameViewModelList.Add(diseaseCodeNameViewModel);
                    }
                }
                foreach (string s in item.YWSX.Split('}'))
                {
                    diseaseCodeNameViewModel = new DiseaseCodeNameViewModel();
                    if (s != "")
                    {
                        diseaseCodeNameViewModel.VDiseaseCode = item.DiseaseCode;
                        diseaseCodeNameViewModel.VDiseaseList = s;
                        diseaseCodeNameViewModel.VDiseaseType = "疾病英文缩写";
                        diseaseCodeNameViewModelList.Add(diseaseCodeNameViewModel);
                    }
                }  
            }
            return Json(diseaseCodeNameViewModelList, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region 根据疾病代码查询
        public ActionResult SearchByCode()
        { 
            string DiseaseCode = CommonFunc.SafeGetStringFromObj(Request["DiseaseCode"]);
            var diseaseInfo = diseaseInfoService.LoadEntities(u=>(u.Status=="1")&&(u.DiseaseCode==DiseaseCode)).FirstOrDefault();
            var modulesContentsList = from mc in diseaseInfo.ModulesContents
                                      orderby mc.ModulesId
                                      select new {
                                         VDiseaseName=diseaseInfo.DiseaseName,
                                         VSpell=diseaseInfo.Spell, 
                                         VYWMC=diseaseInfo.YWMC,
                                         VYWSX=diseaseInfo.YWSX,
                                         VId = mc.Id,
                                         VDiseaseCode=mc.DiseaseCode,
                                         VModulesId=mc.ModulesId,
                                         VModulesContent=mc.ModulesContent,
                                         VCreateTime=mc.CreateTime,
                                         VModulesName=mc.ModulesName
                                      };
            return Json(new{data=modulesContentsList} ,JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region 根据疾病名称查询
        public ActionResult SearchByName()
        { 
            string DiseaseName = CommonFunc.SafeGetStringFromObj(Request["DiseaseName"]);
            var diseaseInfo = diseaseInfoService.LoadEntities(u =>(u.Status=="1")&&((u.DiseaseName.Contains(DiseaseName))
                || (u.Spell.Contains(DiseaseName))
                || (u.YWMC.Contains(DiseaseName))
                || (u.YWSX.Contains(DiseaseName)))).FirstOrDefault();
            if (diseaseInfo == null)
            {
                return Json(new { status="error",data="未查找到该疾病内容"},JsonRequestBehavior.AllowGet);
            }
            else
            {
                var modulesContentsList = from mc in diseaseInfo.ModulesContents 
                                          orderby mc.ModulesId
                                          select new 
                                          {
                                              VDiseaseName = diseaseInfo.DiseaseName,
                                              VSpell = diseaseInfo.Spell,
                                              VYWMC = diseaseInfo.YWMC,
                                              VYWSX = diseaseInfo.YWSX,
                                              VId = mc.Id,
                                              VDiseaseCode = mc.DiseaseCode,
                                              VModulesId = mc.ModulesId,
                                              VModulesContent = mc.ModulesContent,
                                              VCreateTime = mc.CreateTime,
                                              VModulesName = mc.ModulesName
                                          };
                return Json(new { status = "ok", data = modulesContentsList}, JsonRequestBehavior.AllowGet);
            }  
        }
        #endregion


    }
}
