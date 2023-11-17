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
    public interface IMesMovementsRepository
    {
        public Task<MesMovementsDTO> GetById(Guid id);
        public Task<IEnumerable<MesMovementsDTO>> GetAllByTimeInterval(DateTime? startTime, DateTime? endTime, string intervalMode);        
        public Task<MesMovementsDTO> Update(MesMovementsDTO objDTO);
        public Task<MesMovementsDTO> Create(MesMovementsDTO objectToAddDTO);
        public Task<int> Delete(Guid id);
    }
}
