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
    public interface ICorrectionReasonRepository
    {
        public Task<CorrectionReasonDTO> Get(int Id);
        public Task<IEnumerable<CorrectionReasonDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);
        public Task<CorrectionReasonDTO> Update(CorrectionReasonDTO objDTO, UpdateMode updateMode = UpdateMode.Update);
        public Task<CorrectionReasonDTO> Create(CorrectionReasonDTO objectToAddDTO);
    }
}
