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
    public interface IReportEntityRepository
    {
        public Task<ReportEntityDTO> GetById(Guid id);
        public Task<IEnumerable<ReportEntityDTO>> GetAll();
        public Task<IEnumerable<ReportEntityDTO>> GetAllByDownloadTimeInterval(DateTime? startDownloadTime, DateTime? endDownloadTime);
        public Task<IEnumerable<ReportEntityDTO>> GetAllByUploadTimeInterval(DateTime? startUploadTime, DateTime? endUploadTime);
        public Task<ReportEntityDTO> Update(ReportEntityDTO objDTO);
        public Task<ReportEntityDTO> Create(ReportEntityDTO objectToAddDTO);
        public Task<int> Delete(Guid id);        
    }
}
