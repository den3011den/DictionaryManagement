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
    public interface IMesDepartmentRepository
    {
        public Task<MesDepartmentDTO> GetById(int mesDepartmentId);
        public Task<IEnumerable<MesDepartmentDTO>> GetChildList(int mesDepartmentId);
        public Task<IEnumerable<MesDepartmentDTO>> GetAll();
        public Task<MesDepartmentDTO> Update(MesDepartmentDTO objDTO);
        public Task<MesDepartmentDTO> Create(MesDepartmentDTO objectToAddDTO);
        public Task<int> Delete(int mesDepartmentId);
    }
}
