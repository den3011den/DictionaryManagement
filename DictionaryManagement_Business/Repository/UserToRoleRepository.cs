using AutoMapper;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository
{
    public class UserToRoleRepository : IUserToRoleRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserToRoleRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserToRoleDTO> Create(UserToRoleDTO objectToAddDTO)
        {

            UserToRole objectToAdd = new UserToRole();
                
                objectToAdd.Id = objectToAddDTO.Id;
                objectToAdd.UserId = objectToAddDTO.UserId;
                objectToAdd.RoleId = objectToAddDTO.RoleId;


            var addedUserToRole = _db.UserToRole.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<UserToRole, UserToRoleDTO>(addedUserToRole.Entity);
        }

        public async Task<UserToRoleDTO> Get(string userId, string roleId)
        {
            var objToGet = _db.UserToRole.Include("UserFK").Include("RoleFK").
                            FirstOrDefaultAsync(u => u.UserId.Trim().ToUpper() == userId.Trim().ToUpper() && u.RoleId.Trim().ToUpper() == roleId.Trim().ToUpper()).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<UserToRole, UserToRoleDTO>(objToGet);
            }
            return null;
        }

        public async Task<UserToRoleDTO> GetById(int id)
        {
            var objToGet = _db.UserToRole.Include("UserFK").Include("RoleFK").
                            FirstOrDefaultAsync(u => u.Id == id).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<UserToRole, UserToRoleDTO>(objToGet);
            }
            return null;
        }


        public async Task<IEnumerable<UserToRoleDTO>> GetAll()
        {
            var hhh = _db.UserToRole.Include("UserFK").Include("RoleFK");
            return _mapper.Map<IEnumerable<UserToRole>, IEnumerable<UserToRoleDTO>>(hhh);
            
        }

        public async Task<UserToRoleDTO> Update(UserToRoleDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.UserToRole.Include("UserFK").Include("RoleFK").
                    FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {

                if (objectToUpdate.UserId != objectToUpdateDTO.UserDTOFK.Id)
                {
                    objectToUpdate.UserId = objectToUpdateDTO.UserDTOFK.Id;                    
                    objectToUpdate.UserFK = _mapper.Map<UserDTO, User>(objectToUpdateDTO.UserDTOFK);
                }
                if (objectToUpdate.RoleId != objectToUpdateDTO.RoleDTOFK.Id)
                {
                    objectToUpdate.RoleId = objectToUpdateDTO.RoleDTOFK.Id;
                    objectToUpdate.RoleFK = _mapper.Map<RoleDTO, Role>(objectToUpdateDTO.RoleDTOFK);
                }
                _db.UserToRole.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<UserToRole, UserToRoleDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(int id)
        {
            if (id > 0)
            {
                var objectToDelete = _db.UserToRole.FirstOrDefault(u => u.Id == id);
                if (objectToDelete != null)
                {
                    _db.UserToRole.Remove(objectToDelete);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;

        }
    }
}
