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
    public interface IErrorCriterionRepository
    {
        public Task<ErrorCriterionDTO> Get(int Id);
        public Task<IEnumerable<ErrorCriterionDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);
        public Task<ErrorCriterionDTO> Update(ErrorCriterionDTO objDTO, UpdateMode updateMode = UpdateMode.Update);
        public Task<ErrorCriterionDTO> Create(ErrorCriterionDTO objectToAddDTO);
    }
}
