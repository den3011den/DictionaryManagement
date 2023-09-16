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
    public class UserRepository : IUserRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserDTO> Create(UserDTO objectToAddDTO)
        {
            var objectToAdd = _mapper.Map<UserDTO, User>(objectToAddDTO);
            var addedUser = _db.User.Add(objectToAdd);
            _db.SaveChanges();
            return _mapper.Map<User, UserDTO>(addedUser.Entity);
        }

        public async Task<UserDTO> Get(Guid Id)
        {
            if (Id != null && Id != Guid.Empty)  {
                var objToGet = _db.User.FirstOrDefault(u => u.Id == Id);
                if (objToGet != null)
                {
                    return _mapper.Map<User, UserDTO>(objToGet);
                }
            }
            return null;
        }

        public async Task<IEnumerable<UserDTO>> GetAll(SD.SelectDictionaryScope selectDictionaryScope = SD.SelectDictionaryScope.All)
        {
            if (selectDictionaryScope == SD.SelectDictionaryScope.All)
            {                
                return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_db.User);
            }
            if (selectDictionaryScope == SD.SelectDictionaryScope.ArchiveOnly)
                return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_db.User.Where(u => u.IsArchive == true));
            if (selectDictionaryScope == SD.SelectDictionaryScope.NotArchiveOnly)            
                return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_db.User.Where(u => u.IsArchive != true));
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_db.User);
        }

        public async Task<UserDTO> GetByLogin(string login = "")
        {
            var objToGet = _db.User.FirstOrDefault(u => ((u.Login.Trim().ToUpper() == login.Trim().ToUpper())));
            if (objToGet != null)
            {
                return _mapper.Map<User, UserDTO>(objToGet);
            }
            return null;
        }

        public async Task<UserDTO> GetByLoginNotInArchive(string login = "")
        {
            var objToGet = _db.User.FirstOrDefault(u => ((u.Login.Trim().ToUpper() == login.Trim().ToUpper()))
                && u.IsArchive != true);
            if (objToGet != null)
            {
                return _mapper.Map<User, UserDTO>(objToGet);
            }
            return null;
        }

        public async Task<UserDTO> GetByUserName(string userName = "")
        {
            var objToGet = _db.User.FirstOrDefault(u => ((u.UserName.Trim().ToUpper()) == (userName.Trim().ToUpper())));
            if (objToGet != null)
            {
                return _mapper.Map<User, UserDTO>(objToGet);
            }
            return null;
        }


        public async Task<UserDTO> Update(UserDTO objectToUpdateDTO, SD.UpdateMode updateMode = SD.UpdateMode.Update)
        {
            var objectToUpdate = _db.User.FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {
                if (updateMode == SD.UpdateMode.Update)
                {
                    if (objectToUpdate.Login != objectToUpdateDTO.Login)
                        objectToUpdate.Login = objectToUpdateDTO.Login;
                    if (objectToUpdate.UserName != objectToUpdateDTO.UserName)
                        objectToUpdate.UserName = objectToUpdateDTO.UserName;
                    if (objectToUpdate.Description != objectToUpdateDTO.Description)
                        objectToUpdate.Description = objectToUpdateDTO.Description;
                }
                if (updateMode == SD.UpdateMode.MoveToArchive)
                {
                    objectToUpdate.IsArchive = true;
                }
                if (updateMode == SD.UpdateMode.RestoreFromArchive)
                {
                    objectToUpdate.IsArchive = false;
                }
                _db.User.Update(objectToUpdate);
                _db.SaveChanges();
                return _mapper.Map<User, UserDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;
        }
    }
}
