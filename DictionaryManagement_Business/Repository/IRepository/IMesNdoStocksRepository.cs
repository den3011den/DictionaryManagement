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
    public interface IMesNdoStocksRepository
    {
        public Task<MesNdoStocksDTO> GetById(Int64 id);
        public Task<IEnumerable<MesNdoStocksDTO>> GetAllByTimeInterval(DateTime? startDownloadTime, DateTime? endDownloadTime, string intervalMode);        
        public Task<MesNdoStocksDTO> Update(MesNdoStocksDTO objDTO);
        public Task<MesNdoStocksDTO> Create(MesNdoStocksDTO objectToAddDTO);
        public Task<Int64> Delete(Int64 id);
    }
}
