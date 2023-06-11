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
    public interface IUserToRoleRepository
    {
        public Task<UserToRoleDTO> Get(string userId, string roleId);
        public Task<UserToRoleDTO> GetById(int id);
        public Task<IEnumerable<UserToRoleDTO>> GetAll();
        public Task<UserToRoleDTO> Update(UserToRoleDTO objDTO);
        public Task<UserToRoleDTO> Create(UserToRoleDTO objectToAddDTO);
        public Task<int> Delete(int Id);
    }
}
