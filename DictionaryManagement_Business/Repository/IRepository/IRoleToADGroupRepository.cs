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
    public interface IRoleToADGroupRepository
    {
        public Task<RoleToADGroupDTO> Get(Guid roleId, Guid adGroupId);
        public Task<RoleToADGroupDTO> GetById(int id);
        public Task<IEnumerable<RoleToADGroupDTO>> GetAll();
        public Task<RoleToADGroupDTO> Update(RoleToADGroupDTO objDTO);
        public Task<RoleToADGroupDTO> Create(RoleToADGroupDTO objectToAddDTO);
        public Task<int> Delete(int Id);        
    }
}
