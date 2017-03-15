 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeBase.Add.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace KnowledgeBase.Add.DAL
{
   
		
	public partial class BigCodeNameDal:BaseDal<BigCodeName>
    {
	}
		
	public partial class DeptDal:BaseDal<Dept>
    {
	}
		
	public partial class DiseaseCodeNameDal:BaseDal<DiseaseCodeName>
    {
	}
		
	public partial class DiseaseInfoDal:BaseDal<DiseaseInfo>
    {
	}
		
	public partial class ModulesDal:BaseDal<Modules>
    {
	}
		
	public partial class ModulesContentsDal:BaseDal<ModulesContents>
    {
	}
		
	public partial class SmallCodeNameDal:BaseDal<SmallCodeName>
    {
	}
		
	public partial class TestDal:BaseDal<Test>
    {
	}
		
	public partial class UserInfoDal:BaseDal<UserInfo>
    {
	}
		
	public partial class UserInfoSelectDal:BaseDal<UserInfoSelect>
    {
	}


}