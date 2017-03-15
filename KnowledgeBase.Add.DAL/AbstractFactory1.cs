 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Add.DAL
{
	public partial class AbstractFactory
	{
				
			public static BigCodeNameDal GetBigCodeNameDal()
			{
			  return Assembly.Load(AssemblyPath).CreateInstance(NameSpacePath + ".BigCodeNameDal") as BigCodeNameDal;
			}
	 		
			public static DeptDal GetDeptDal()
			{
			  return Assembly.Load(AssemblyPath).CreateInstance(NameSpacePath + ".DeptDal") as DeptDal;
			}
	 		
			public static DiseaseCodeNameDal GetDiseaseCodeNameDal()
			{
			  return Assembly.Load(AssemblyPath).CreateInstance(NameSpacePath + ".DiseaseCodeNameDal") as DiseaseCodeNameDal;
			}
	 		
			public static DiseaseInfoDal GetDiseaseInfoDal()
			{
			  return Assembly.Load(AssemblyPath).CreateInstance(NameSpacePath + ".DiseaseInfoDal") as DiseaseInfoDal;
			}
	 		
			public static ModulesDal GetModulesDal()
			{
			  return Assembly.Load(AssemblyPath).CreateInstance(NameSpacePath + ".ModulesDal") as ModulesDal;
			}
	 		
			public static ModulesContentsDal GetModulesContentsDal()
			{
			  return Assembly.Load(AssemblyPath).CreateInstance(NameSpacePath + ".ModulesContentsDal") as ModulesContentsDal;
			}
	 		
			public static SmallCodeNameDal GetSmallCodeNameDal()
			{
			  return Assembly.Load(AssemblyPath).CreateInstance(NameSpacePath + ".SmallCodeNameDal") as SmallCodeNameDal;
			}
	 		
			public static TestDal GetTestDal()
			{
			  return Assembly.Load(AssemblyPath).CreateInstance(NameSpacePath + ".TestDal") as TestDal;
			}
	 		
			public static UserInfoDal GetUserInfoDal()
			{
			  return Assembly.Load(AssemblyPath).CreateInstance(NameSpacePath + ".UserInfoDal") as UserInfoDal;
			}
	 		
			public static UserInfoSelectDal GetUserInfoSelectDal()
			{
			  return Assembly.Load(AssemblyPath).CreateInstance(NameSpacePath + ".UserInfoSelectDal") as UserInfoSelectDal;
			}
	 	 }
}