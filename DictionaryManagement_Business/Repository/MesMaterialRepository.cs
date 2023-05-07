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
    public class MesMaterialRepository : IMesMaterialRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public MesMaterialRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<MesMaterialDTO> Create(MesMaterialDTO objectToAddDTO)
        {
            var objectToAdd = _mapper.Map<MesMaterialDTO, MesMaterial>(objectToAddDTO);
            if (_db.MesMaterial.Where(u => u.Id.Trim().ToUpper() == objectToAddDTO.Id.Trim().ToUpper()).Count() <= 0)
            { 
                var addedMesMaterial = _db.MesMaterial.Add(objectToAdd);
                await _db.SaveChangesAsync();
                return _mapper.Map<MesMaterial, MesMaterialDTO>(addedMesMaterial.Entity);
            }
            return new MesMaterialDTO();
        }

        public async Task<MesMaterialDTO> Get(string Id)
        {
            if (!string.IsNullOrEmpty(Id.Trim()))
            {
                var objToGet = _db.MesMaterial.FirstOrDefaultAsync(u => u.Id == Id).GetAwaiter().GetResult();
                if (objToGet != null)
                {
                    return _mapper.Map<MesMaterial, MesMaterialDTO>(objToGet);
                }
            }
            return new MesMaterialDTO();
        }

        public async Task<IEnumerable<MesMaterialDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All)
        {
            if (selectDictionaryScope == SD.SelectDictionaryScope.All)
            {                
                return _mapper.Map<IEnumerable<MesMaterial>, IEnumerable<MesMaterialDTO>>(_db.MesMaterial);
            }
            if (selectDictionaryScope == SD.SelectDictionaryScope.ArchiveOnly)
                return _mapper.Map<IEnumerable<MesMaterial>, IEnumerable<MesMaterialDTO>>(_db.MesMaterial.Where(u => u.IsArchive == true));
            if (selectDictionaryScope == SD.SelectDictionaryScope.NotArchiveOnly)
                return _mapper.Map<IEnumerable<MesMaterial>, IEnumerable<MesMaterialDTO>>(_db.MesMaterial.Where(u => u.IsArchive != true));
            return _mapper.Map<IEnumerable<MesMaterial>, IEnumerable<MesMaterialDTO>>(_db.MesMaterial);
        }

        public async Task<MesMaterialDTO> Update(MesMaterialDTO objectToUpdateDTO, UpdateMode updateMode = UpdateMode.Update)
        {
            var objectToUpdate = _db.MesMaterial.FirstOrDefault(u => u.Id.Trim().ToUpper() == objectToUpdateDTO.Id.Trim().ToUpper());
            if (objectToUpdate != null)
            {
                if (updateMode == SD.UpdateMode.Update)
                {
                    if (objectToUpdate.Id.Trim().ToUpper() != objectToUpdateDTO.Id.Trim().ToUpper())
                        objectToUpdate.Id = objectToUpdateDTO.Id.Trim();
                    if (objectToUpdate.Name != objectToUpdateDTO.Name)
                        objectToUpdate.Name = objectToUpdateDTO.Name;
                    if (objectToUpdate.ShortName != objectToUpdateDTO.ShortName)
                        objectToUpdate.ShortName = objectToUpdateDTO.ShortName;
                }
                if (updateMode == SD.UpdateMode.MoveToArchive)
                {
                    objectToUpdate.IsArchive = true;
                }
                if (updateMode == SD.UpdateMode.RestoreFromArchive)
                {
                    objectToUpdate.IsArchive = false;
                }
                _db.MesMaterial.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<MesMaterial, MesMaterialDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }
    }
}
