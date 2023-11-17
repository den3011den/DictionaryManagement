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
    public interface ISapMovementsOUTRepository
    {
        public Task<SapMovementsOUTDTO> GetById(Guid id);
        public Task<IEnumerable<SapMovementsOUTDTO>> GetAllByTimeInterval(DateTime? startTime, DateTime? endTime, string intervalMode);        
        public Task<SapMovementsOUTDTO> Update(SapMovementsOUTDTO objDTO);
        public Task<SapMovementsOUTDTO> Create(SapMovementsOUTDTO objectToAddDTO);
        public Task<int> Delete(Guid id);
    }
}
