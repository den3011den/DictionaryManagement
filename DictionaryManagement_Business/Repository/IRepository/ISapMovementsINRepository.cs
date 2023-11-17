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
    public interface ISapMovementsINRepository
    {
        public Task<SapMovementsINDTO> GetById(string erpId);
        public Task<IEnumerable<SapMovementsINDTO>> GetAllByTimeInterval(DateTime? startTime, DateTime? endTime, string intervalMode);        
        public Task<SapMovementsINDTO> Update(SapMovementsINDTO objDTO);
        public Task<SapMovementsINDTO> Create(SapMovementsINDTO objectToAddDTO);
        public Task<int> Delete(string erpId);
    }
}
