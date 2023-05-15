using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository.IRepository
{
    public interface ISapToMesMaterialMappingRepository
    {
        public Task<SapToMesMaterialMappingDTO> Get(int sapMaterialId, int mesMaterialId);
        public Task<SapToMesMaterialMappingDTO> GetById(int id);
        public Task<IEnumerable<SapToMesMaterialMappingDTO>> GetAll();
        public Task<SapToMesMaterialMappingDTO> Update(SapToMesMaterialMappingDTO objDTO);
        public Task<SapToMesMaterialMappingDTO> Create(SapToMesMaterialMappingDTO objectToAddDTO);
        public Task<int> Delete(int Id);
    }
}
