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
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        DbContext Db = DbContextFactory.CreateDbContext();

        #region 密码修改
        public bool UpdatePwd(UserInfo userInfoModel)
        {
            Db.Set<UserInfo>().Add(userInfoModel);
            DbEntityEntry<UserInfo> entry = Db.Entry<UserInfo>(userInfoModel);
            entry.State = EntityState.Unchanged;
            entry.Property(u => u.Password).IsModified = true;

            Db.Configuration.ValidateOnSaveEnabled = false;
            int count = Db.SaveChanges();
            Db.Configuration.ValidateOnSaveEnabled = true;
            return count > 0;
        } 
        #endregion

    }
}
