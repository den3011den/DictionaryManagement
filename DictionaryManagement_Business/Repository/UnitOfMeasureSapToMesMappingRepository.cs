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
    public class UnitOfMeasureSapToMesMappingRepository : IUnitOfMeasureSapToMesMappingRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UnitOfMeasureSapToMesMappingRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UnitOfMeasureSapToMesMappingDTO> Create(UnitOfMeasureSapToMesMappingDTO objectToAddDTO)
        {
            //var objectToAdd = _mapper.Map<UnitOfMeasureSapToMesMappingDTO, UnitOfMeasureSapToMesMapping>(objectToAddDTO);

            UnitOfMeasureSapToMesMapping objectToAdd = new UnitOfMeasureSapToMesMapping();
                
                objectToAdd.Id = objectToAddDTO.Id;
                objectToAdd.SapUnitId = objectToAddDTO.SapUnitId;
                objectToAdd.MesUnitId = objectToAddDTO.MesUnitId;


            var addedUnitOfMeasureSapToMesMapping = _db.UnitOfMeasureSapToMesMapping.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<UnitOfMeasureSapToMesMapping, UnitOfMeasureSapToMesMappingDTO>(addedUnitOfMeasureSapToMesMapping.Entity);
        }

        public async Task<UnitOfMeasureSapToMesMappingDTO> Get(int sapUnitId, int mesUnitId)
        {
            var objToGet = _db.UnitOfMeasureSapToMesMapping.Include("SapUnitOfMeasure").Include("MesUnitOfMeasure").
                            FirstOrDefaultAsync(u => u.SapUnitId == sapUnitId && u.MesUnitId == mesUnitId).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<UnitOfMeasureSapToMesMapping, UnitOfMeasureSapToMesMappingDTO>(objToGet);
            }
            return null;
        }

        public async Task<UnitOfMeasureSapToMesMappingDTO> GetById(int id)
        {
            var objToGet = _db.UnitOfMeasureSapToMesMapping.Include("SapUnitOfMeasure").Include("MesUnitOfMeasure").
                            FirstOrDefaultAsync(u => u.Id == id).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<UnitOfMeasureSapToMesMapping, UnitOfMeasureSapToMesMappingDTO>(objToGet);
            }
            return null;
        }


        public async Task<IEnumerable<UnitOfMeasureSapToMesMappingDTO>> GetAll()
        {
            var hhh = _db.UnitOfMeasureSapToMesMapping.Include("SapUnitOfMeasure").Include("MesUnitOfMeasure");           
            return _mapper.Map<IEnumerable<UnitOfMeasureSapToMesMapping>, IEnumerable<UnitOfMeasureSapToMesMappingDTO>>(hhh);
        }

        public async Task<UnitOfMeasureSapToMesMappingDTO> Update(UnitOfMeasureSapToMesMappingDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.UnitOfMeasureSapToMesMapping.Include("SapUnitOfMeasure").Include("MesUnitOfMeasure").
                    FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {
                if (objectToUpdate.SapUnitId != objectToUpdateDTO.SapUnitOfMeasureDTO.Id)
                {
                    objectToUpdate.SapUnitId = objectToUpdateDTO.SapUnitOfMeasureDTO.Id;
                    objectToUpdate.SapUnitOfMeasure = _mapper.Map<SapUnitOfMeasureDTO, SapUnitOfMeasure>(objectToUpdateDTO.SapUnitOfMeasureDTO);
                }
                if (objectToUpdate.MesUnitId != objectToUpdateDTO.MesUnitOfMeasureDTO.Id)
                {
                    objectToUpdate.MesUnitId = objectToUpdateDTO.MesUnitOfMeasureDTO.Id;
                    objectToUpdate.MesUnitOfMeasure = _mapper.Map<MesUnitOfMeasureDTO, MesUnitOfMeasure>(objectToUpdateDTO.MesUnitOfMeasureDTO);
                }
                if (objectToUpdate.SapToMesTransformKoef != objectToUpdateDTO.SapToMesTransformKoef)
                    objectToUpdate.SapToMesTransformKoef = objectToUpdateDTO.SapToMesTransformKoef;
                _db.UnitOfMeasureSapToMesMapping.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<UnitOfMeasureSapToMesMapping, UnitOfMeasureSapToMesMappingDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(int id)
        {
            if (id > 0)
            {
                var objectToDelete = _db.UnitOfMeasureSapToMesMapping.FirstOrDefault(u => u.Id == id);
                if (objectToDelete != null)
                {
                    _db.UnitOfMeasureSapToMesMapping.Remove(objectToDelete);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;

        }
    }
}
