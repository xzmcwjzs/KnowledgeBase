 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeBase.Add.Model;

namespace KnowledgeBase.Add.IBLL
{
	
    public partial interface IBigCodeNameService:IBaseService<BigCodeName>
    {
    }
	
    public partial interface IDeptService:IBaseService<Dept>
    {
    }
	
    public partial interface IDiseaseCodeNameService:IBaseService<DiseaseCodeName>
    {
    }
	
    public partial interface IDiseaseInfoService:IBaseService<DiseaseInfo>
    {
    }
	
    public partial interface IModulesService:IBaseService<Modules>
    {
    }
	
    public partial interface IModulesContentsService:IBaseService<ModulesContents>
    {
    }
	
    public partial interface ISmallCodeNameService:IBaseService<SmallCodeName>
    {
    }
	
    public partial interface ITestService:IBaseService<Test>
    {
    }
	
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
    }
	
    public partial interface IUserInfoSelectService:IBaseService<UserInfoSelect>
    {
    }
}