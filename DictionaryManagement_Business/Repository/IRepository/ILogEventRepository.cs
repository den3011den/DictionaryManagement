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
    public interface ILogEventRepository
    {
        public Task<IEnumerable<LogEventDTO>> GetAllByTimeInterval(DateTime? startEventTime, DateTime? endEventTime);
        public Task<IEnumerable<LogEventDTO>> GetAllByReportEntityId(Guid reportEntityId);
    }
}
