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
    public interface IMesMaterialRepository
    {
        public Task<MesMaterialDTO> Get(string Id);
        public Task<IEnumerable<MesMaterialDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);
        public Task<MesMaterialDTO> Update(MesMaterialDTO objDTO, UpdateMode updateMode = UpdateMode.Update);
        public Task<MesMaterialDTO> Create(MesMaterialDTO objectToAddDTO);
    }
}
