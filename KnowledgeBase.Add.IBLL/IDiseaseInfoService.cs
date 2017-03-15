using KnowledgeBase.Add.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Add.IBLL
{
    public partial interface IDiseaseInfoService : IBaseService<DiseaseInfo>
    {
        bool Update(DiseaseInfo diseaseInfoModel);
        bool TiJiaoSH(DiseaseInfo diseaseInfoModel);
        bool TiJiaoJD(DiseaseInfo diseaseInfoModel);
        bool LuRuSH(DiseaseInfo diseaseInfoModel);
    }
}
