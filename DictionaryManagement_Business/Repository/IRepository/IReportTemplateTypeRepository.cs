using DictionaryManagement_Common;
using DictionaryManagement_Models.IntDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository.IRepository
{
    public interface IReportTemplateTypeRepository
    {
        public Task<ReportTemplateTypeDTO> Get(int Id);
        public Task<IEnumerable<ReportTemplateTypeDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);
        public Task<ReportTemplateTypeDTO> Update(ReportTemplateTypeDTO objDTO, UpdateMode updateMode = UpdateMode.Update);
        public Task<ReportTemplateTypeDTO> Create(ReportTemplateTypeDTO objectToAddDTO);
    }
}
