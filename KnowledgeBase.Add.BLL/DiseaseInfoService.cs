using KnowledgeBase.Add.DAL;
using KnowledgeBase.Add.IBLL;
using KnowledgeBase.Add.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Add.BLL
{
    public partial class DiseaseInfoService : BaseService<DiseaseInfo>, IDiseaseInfoService
    {
        DbContext Db = DbContextFactory.CreateDbContext();
        public bool Update(DiseaseInfo diseaseInfoModel)
        {
            throw new NotImplementedException();
        }


        #region 提交审核结果
        public bool TiJiaoSH(DiseaseInfo diseaseInfoModel)
        {
            Db.Set<DiseaseInfo>().Add(diseaseInfoModel);
            DbEntityEntry<DiseaseInfo> entry = Db.Entry<DiseaseInfo>(diseaseInfoModel);
            entry.State = EntityState.Unchanged;
            entry.Property(u => u.ShenHe).IsModified = true;
            entry.Property(u => u.ShenHeId).IsModified = true;
            entry.Property(u => u.Status).IsModified = true;
            Db.Configuration.ValidateOnSaveEnabled = false;
            int count = Db.SaveChanges();
            Db.Configuration.ValidateOnSaveEnabled = true;
            return count > 0;
        } 
        #endregion

        #region 录入人本人提交审核结果
        public bool LuRuSH(DiseaseInfo diseaseInfoModel)
        {
            Db.Set<DiseaseInfo>().Add(diseaseInfoModel);
            DbEntityEntry<DiseaseInfo> entry = Db.Entry<DiseaseInfo>(diseaseInfoModel);
            entry.State = EntityState.Unchanged; 
            entry.Property(u => u.Status).IsModified = true;
            Db.Configuration.ValidateOnSaveEnabled = false;
            int count = Db.SaveChanges();
            Db.Configuration.ValidateOnSaveEnabled = true;
            return count > 0;
        } 
        #endregion



        #region 提交校对名单
        public bool TiJiaoJD(DiseaseInfo diseaseInfoModel)
        {
           Db.Set<DiseaseInfo>().Add(diseaseInfoModel);
            DbEntityEntry<DiseaseInfo> entry = Db.Entry<DiseaseInfo>(diseaseInfoModel);
            entry.State = EntityState.Unchanged;
            entry.Property(u => u.JiaoDui).IsModified = true;
            entry.Property(u => u.JiaoDuiId).IsModified = true;
            entry.Property(u => u.JDStatus).IsModified = true;
            Db.Configuration.ValidateOnSaveEnabled = false;
            int count = Db.SaveChanges();
            Db.Configuration.ValidateOnSaveEnabled = true;
            return count > 0;
        } 
        #endregion
    }
}
