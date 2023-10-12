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
    public interface IDataTypeRepository
    {
        public Task<DataTypeDTO> Get(int Id);
        public Task<IEnumerable<DataTypeDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);
        public Task<DataTypeDTO> Update(DataTypeDTO objDTO, UpdateMode updateMode = UpdateMode.Update);
        public Task<DataTypeDTO> Create(DataTypeDTO objectToAddDTO);
        public Task<DataTypeDTO> GetByName(string Name);
        public Task<DataTypeDTO> GetByPriority(int? priority);
    }
}
