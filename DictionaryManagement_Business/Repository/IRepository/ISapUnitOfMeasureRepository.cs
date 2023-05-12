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
    public interface ISapUnitOfMeasureRepository
    {
        public Task<SapUnitOfMeasureDTO> Get(int Id);
        public Task<IEnumerable<SapUnitOfMeasureDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);
        public Task<SapUnitOfMeasureDTO> Update(SapUnitOfMeasureDTO objDTO, UpdateMode updateMode = UpdateMode.Update);
        public Task<SapUnitOfMeasureDTO> Create(SapUnitOfMeasureDTO objectToAddDTO);
        public Task<SapUnitOfMeasureDTO> GetByShortName(string shortName);
        public Task<SapUnitOfMeasureDTO> GetByName(string Name);
    }
}
