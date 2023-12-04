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
    public interface IReportEntityLogRepository
    {
        public Task<ReportEntityLogDTO> GetById(Int64 id);
        public Task<IEnumerable<ReportEntityLogDTO>> GetAll();
        public Task<IEnumerable<ReportEntityLogDTO>> GetByReportEntityId(Guid reportEntityId);
        public Task<IEnumerable<ReportEntityLogDTO>> GetAllByLogTimeInterval(DateTime? startLogTime, DateTime? endLogTime);        
        public Task<ReportEntityLogDTO> Update(ReportEntityLogDTO objDTO);
        public Task<ReportEntityLogDTO> Create(ReportEntityLogDTO objectToAddDTO);
        public Task<int> Delete(Int64 id);
        public Task<IEnumerable<ReportEntityLogDTO>> GetAllByReportEntityId(Guid reportEntityId);
    }
}
