 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeBase.Add.DAL;
using KnowledgeBase.Add.IBLL;
using KnowledgeBase.Add.Model;

namespace KnowledgeBase.Add.BLL
{
	
	public partial class BigCodeNameService:BaseService<BigCodeName>,IBigCodeNameService
    {	
		public override void SetCurrentDal()
        {
            CurrentDal = AbstractFactory.GetBigCodeNameDal();
        }
	}
	
	public partial class DeptService:BaseService<Dept>,IDeptService
    {	
		public override void SetCurrentDal()
        {
            CurrentDal = AbstractFactory.GetDeptDal();
        }
	}
	
	public partial class DiseaseCodeNameService:BaseService<DiseaseCodeName>,IDiseaseCodeNameService
    {	
		public override void SetCurrentDal()
        {
            CurrentDal = AbstractFactory.GetDiseaseCodeNameDal();
        }
	}
	
	public partial class DiseaseInfoService:BaseService<DiseaseInfo>,IDiseaseInfoService
    {	
		public override void SetCurrentDal()
        {
            CurrentDal = AbstractFactory.GetDiseaseInfoDal();
        }
	}
	
	public partial class ModulesService:BaseService<Modules>,IModulesService
    {	
		public override void SetCurrentDal()
        {
            CurrentDal = AbstractFactory.GetModulesDal();
        }
	}
	
	public partial class ModulesContentsService:BaseService<ModulesContents>,IModulesContentsService
    {	
		public override void SetCurrentDal()
        {
            CurrentDal = AbstractFactory.GetModulesContentsDal();
        }
	}
	
	public partial class SmallCodeNameService:BaseService<SmallCodeName>,ISmallCodeNameService
    {	
		public override void SetCurrentDal()
        {
            CurrentDal = AbstractFactory.GetSmallCodeNameDal();
        }
	}
	
	public partial class TestService:BaseService<Test>,ITestService
    {	
		public override void SetCurrentDal()
        {
            CurrentDal = AbstractFactory.GetTestDal();
        }
	}
	
	public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {	
		public override void SetCurrentDal()
        {
            CurrentDal = AbstractFactory.GetUserInfoDal();
        }
	}
	
	public partial class UserInfoSelectService:BaseService<UserInfoSelect>,IUserInfoSelectService
    {	
		public override void SetCurrentDal()
        {
            CurrentDal = AbstractFactory.GetUserInfoSelectDal();
        }
	}
}