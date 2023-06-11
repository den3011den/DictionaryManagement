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
    public interface IUserToDepartmentRepository
    {
        public Task<UserToDepartmentDTO> Get(string userId, int departmentId);
        public Task<UserToDepartmentDTO> GetById(int id);
        public Task<IEnumerable<UserToDepartmentDTO>> GetAll();
        public Task<UserToDepartmentDTO> Update(UserToDepartmentDTO objDTO);
        public Task<UserToDepartmentDTO> Create(UserToDepartmentDTO objectToAddDTO);
        public Task<int> Delete(int Id);
    }
}
