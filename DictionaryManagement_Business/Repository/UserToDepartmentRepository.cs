using AutoMapper;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using DND.EFCoreWithNoLock.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository
{
    public class UserToDepartmentRepository : IUserToDepartmentRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserToDepartmentRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserToDepartmentDTO> Create(UserToDepartmentDTO objectToAddDTO)
        {

            UserToDepartment objectToAdd = new UserToDepartment();
                
                objectToAdd.Id = objectToAddDTO.Id;
                objectToAdd.UserId = objectToAddDTO.UserId;
                objectToAdd.DepartmentId = objectToAddDTO.DepartmentId;


            var addedUserToDepartment = _db.UserToDepartment.Add(objectToAdd);
            _db.SaveChanges();
            return _mapper.Map<UserToDepartment, UserToDepartmentDTO>(addedUserToDepartment.Entity);
        }

        public async Task<UserToDepartmentDTO> Get(Guid userId, int departmentId)
        {
            var objToGet = _db.UserToDepartment.Include("UserFK").Include("DepartmentFK").
                            FirstOrDefaultWithNoLock(u => u.UserId == userId && u.DepartmentId == departmentId);
            if (objToGet != null)
            {
                return _mapper.Map<UserToDepartment, UserToDepartmentDTO>(objToGet);
            }
            return null;
        }

        public async Task<UserToDepartmentDTO> GetById(int id)
        {
            var objToGet = _db.UserToDepartment.Include("UserFK").Include("DepartmentFK").
                            FirstOrDefaultWithNoLock(u => u.Id == id);
            if (objToGet != null)
            {
                return _mapper.Map<UserToDepartment, UserToDepartmentDTO>(objToGet);
            }
            return null;
        }


        public async Task<IEnumerable<UserToDepartmentDTO>> GetAll()
        {
            var hhh = _db.UserToDepartment.Include("UserFK").Include("DepartmentFK").ToListWithNoLock();
            return _mapper.Map<IEnumerable<UserToDepartment>, IEnumerable<UserToDepartmentDTO>>(hhh);
            
        }

        public async Task<UserToDepartmentDTO> Update(UserToDepartmentDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.UserToDepartment.Include("UserFK").Include("DeaprtmentFK").
                    FirstOrDefaultWithNoLock(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {

                if (objectToUpdate.UserId != objectToUpdateDTO.UserDTOFK.Id)
                {
                    objectToUpdate.UserId = objectToUpdateDTO.UserDTOFK.Id;                    
                    objectToUpdate.UserFK = _mapper.Map<UserDTO, User>(objectToUpdateDTO.UserDTOFK);
                }
                if (objectToUpdate.DepartmentId != objectToUpdateDTO.DepartmentDTOFK.Id)
                {
                    objectToUpdate.DepartmentId = objectToUpdateDTO.DepartmentDTOFK.Id;
                    objectToUpdate.DepartmentFK = _mapper.Map<MesDepartmentDTO, MesDepartment>(objectToUpdateDTO.DepartmentDTOFK);
                }
                _db.UserToDepartment.Update(objectToUpdate);
                _db.SaveChanges();
                return _mapper.Map<UserToDepartment, UserToDepartmentDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(int id)
        {
            if (id > 0)
            {
                var objectToDelete = _db.UserToDepartment.FirstOrDefaultWithNoLock(u => u.Id == id);
                if (objectToDelete != null)
                {
                    _db.UserToDepartment.Remove(objectToDelete);
                    return _db.SaveChanges();
                }
            }
            return 0;

        }
    }
}
