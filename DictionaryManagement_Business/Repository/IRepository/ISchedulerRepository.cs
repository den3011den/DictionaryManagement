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
    public interface ISchedulerRepository
    {
        
        public Task<SchedulerDTO> GetById(int id);
        public Task<IEnumerable<SchedulerDTO>> GetAll();
        public Task<SchedulerDTO> Update(SchedulerDTO objDTO);
        public Task<SchedulerDTO> Create(SchedulerDTO objectToAddDTO);
        public Task<int> Delete(int id);
    }
}
