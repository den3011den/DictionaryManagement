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
    public interface ISmenaRepository
    {
        public Task<SmenaDTO> GetById(int id);
        public Task<IEnumerable<SmenaDTO>> GetAll();
        public Task<SmenaDTO> Update(SmenaDTO objDTO);
        public Task<SmenaDTO> Create(SmenaDTO objectToAddDTO);
        public Task<int> Delete(int id);
    }
}
