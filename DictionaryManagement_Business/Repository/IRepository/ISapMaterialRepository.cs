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
    public interface ISapMaterialRepository
    {
        public Task<SapMaterialDTO> Get(string Id);
        public Task<IEnumerable<SapMaterialDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);
        public Task<SapMaterialDTO> Update(SapMaterialDTO objDTO, UpdateMode updateMode = UpdateMode.Update);
        public Task<SapMaterialDTO> Create(SapMaterialDTO objectToAddDTO);

    }
}
