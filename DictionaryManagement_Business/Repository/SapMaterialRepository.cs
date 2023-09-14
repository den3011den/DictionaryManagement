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
    public class SapMaterialRepository : ISapMaterialRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public SapMaterialRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<SapMaterialDTO> Create(SapMaterialDTO objectToAddDTO)
        {
            var objectToAdd = _mapper.Map<SapMaterialDTO, SapMaterial>(objectToAddDTO);
            var addedSapMaterial = _db.SapMaterial.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<SapMaterial, SapMaterialDTO>(addedSapMaterial.Entity);
        }

        public async Task<SapMaterialDTO> Get(int id)
        {
            if (id > 0)
            {
                var objToGet = await _db.SapMaterial.FirstOrDefaultAsync(u => u.Id == id);
                if (objToGet != null)
                {
                    return _mapper.Map<SapMaterial, SapMaterialDTO>(objToGet);
                }
            }
            return null;
        }


        public async Task<SapMaterialDTO> GetByCode(string code = "")
        {
            var objToGet = await _db.SapMaterial.FirstOrDefaultAsync(u => u.Code.Trim().ToUpper() == code.Trim().ToUpper());
            if (objToGet != null)
            {
                return _mapper.Map<SapMaterial, SapMaterialDTO>(objToGet);
            }
            return null;
        }

        public async Task<SapMaterialDTO> GetByName(string name = "")
        {
            var objToGet = await _db.SapMaterial.FirstOrDefaultAsync(u => u.Name.Trim().ToUpper() == name.Trim().ToUpper());
            if (objToGet != null)
            {
                return _mapper.Map<SapMaterial, SapMaterialDTO>(objToGet);
            }
            return null;
        }

        public async Task<SapMaterialDTO> GetByShortName(string shortName = "")
        {
            var objToGet = await _db.SapMaterial.FirstOrDefaultAsync(u => u.ShortName.Trim().ToUpper() == shortName.Trim().ToUpper());
            if (objToGet != null)
            {
                return _mapper.Map<SapMaterial, SapMaterialDTO>(objToGet);
            }
            return null;
        }

        public async Task<IEnumerable<SapMaterialDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All)
        {
            if (selectDictionaryScope == SD.SelectDictionaryScope.All)
            {                
                return _mapper.Map<IEnumerable<SapMaterial>, IEnumerable<SapMaterialDTO>>(_db.SapMaterial);
            }
            if (selectDictionaryScope == SD.SelectDictionaryScope.ArchiveOnly)
                return _mapper.Map<IEnumerable<SapMaterial>, IEnumerable<SapMaterialDTO>>(_db.SapMaterial.Where(u => u.IsArchive == true));
            if (selectDictionaryScope == SD.SelectDictionaryScope.NotArchiveOnly)
                return _mapper.Map<IEnumerable<SapMaterial>, IEnumerable<SapMaterialDTO>>(_db.SapMaterial.Where(u => u.IsArchive != true));
            return _mapper.Map<IEnumerable<SapMaterial>, IEnumerable<SapMaterialDTO>>(_db.SapMaterial);
        }

        public async Task<SapMaterialDTO> Update(SapMaterialDTO objectToUpdateDTO, UpdateMode updateMode = UpdateMode.Update)
        {
            var objectToUpdate = _db.SapMaterial.FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {
                if (updateMode == SD.UpdateMode.Update)
                {
                    if (objectToUpdate.Code != objectToUpdateDTO.Code)
                        objectToUpdate.Code = objectToUpdateDTO.Code;
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
                _db.SapMaterial.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<SapMaterial, SapMaterialDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }
    }
}
