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
    public interface ISapNdoOUTRepository
    {
        public Task<SapNdoOUTDTO> GetById(Int64 id);
        public Task<IEnumerable<SapNdoOUTDTO>> GetAllByTimeInterval(DateTime? startDownloadTime, DateTime? endDownloadTime, string intervalMode);        
        public Task<SapNdoOUTDTO> Update(SapNdoOUTDTO objDTO);
        public Task<SapNdoOUTDTO> Create(SapNdoOUTDTO objectToAddDTO);
        public Task<Int64> Delete(Int64 id);
    }
}
