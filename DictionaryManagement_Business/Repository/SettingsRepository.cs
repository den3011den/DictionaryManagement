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
    public class SettingsRepository : ISettingsRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public SettingsRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<SettingsDTO> Create(SettingsDTO objectToAddDTO)
        {
            var objectToAdd = _mapper.Map<SettingsDTO, Settings>(objectToAddDTO);
            var addedSettings = _db.Settings.Add(objectToAdd);
            _db.SaveChanges();
            return _mapper.Map<Settings, SettingsDTO>(addedSettings.Entity);
        }


        public async Task<SettingsDTO> Get(int Id)
        {
            var objToGet = _db.Settings.FirstOrDefault(u => u.Id == Id);
            if (objToGet != null)
            {
                return _mapper.Map<Settings, SettingsDTO>(objToGet);
            }
            return null;
        }

        public async Task<SettingsDTO> GetByName(string name)
        {
            var objToGet = _db.Settings.FirstOrDefault(u => u.Name.Trim().ToUpper() == name.Trim().ToUpper());
            if (objToGet != null)
            {
                return _mapper.Map<Settings, SettingsDTO>(objToGet);
            }
            return null;
        }

        public async Task<IEnumerable<SettingsDTO>> GetAll()
        {                      
            return _mapper.Map<IEnumerable<Settings>, IEnumerable<SettingsDTO>>(_db.Settings);
        }

        public async Task<SettingsDTO> Update(SettingsDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.Settings.FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {                

                    if (objectToUpdate.Name != objectToUpdateDTO.Name)
                        objectToUpdate.Name = objectToUpdateDTO.Name;
                    if (objectToUpdate.Description != objectToUpdateDTO.Description)
                        objectToUpdate.Description = objectToUpdateDTO.Description;
                    if (objectToUpdate.Value != objectToUpdateDTO.Value)
                        objectToUpdate.Value = objectToUpdateDTO.Value;
                _db.Settings.Update(objectToUpdate);
                _db.SaveChanges();
                return _mapper.Map<Settings, SettingsDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(int id)
        {
            if (id > 0)
            {
                var objectToDelete = _db.Settings.FirstOrDefault(u => u.Id == id);
                if (objectToDelete!= null)
                {
                    _db.Settings.Remove(objectToDelete);
                    return _db.SaveChanges();
                }
            }
            return 0;
            
        }
    }
}
