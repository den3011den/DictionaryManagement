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
    public class SapToMesMaterialMappingRepository : ISapToMesMaterialMappingRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public SapToMesMaterialMappingRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<SapToMesMaterialMappingDTO> Create(SapToMesMaterialMappingDTO objectToAddDTO)
        {
            //var objectToAdd = _mapper.Map<UnitOfMeasureSapToMesMappingDTO, UnitOfMeasureSapToMesMapping>(objectToAddDTO);

            SapToMesMaterialMapping objectToAdd = new SapToMesMaterialMapping();
                
                objectToAdd.Id = objectToAddDTO.Id;
                objectToAdd.SapMaterialId = objectToAddDTO.SapMaterialId;
                objectToAdd.MesMaterialId = objectToAddDTO.MesMaterialId;


            var addedSapToMesMaterialMapping = _db.SapToMesMaterialMapping.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<SapToMesMaterialMapping, SapToMesMaterialMappingDTO>(addedSapToMesMaterialMapping.Entity);
        }

        public async Task<SapToMesMaterialMappingDTO> Get(int sapMaterialId, int mesMaterialId)
        {
            var objToGet = await _db.SapToMesMaterialMapping.Include("SapMaterial").Include("MesMaterial").
                            FirstOrDefaultAsync(u => u.SapMaterialId == sapMaterialId && u.MesMaterialId == mesMaterialId);
            if (objToGet != null)
            {
                return _mapper.Map<SapToMesMaterialMapping, SapToMesMaterialMappingDTO>(objToGet);
            }
            return null;
        }

        public async Task<SapToMesMaterialMappingDTO> GetById(int id)
        {
            var objToGet = await _db.SapToMesMaterialMapping.Include("SapMaterial").Include("MesMaterial").
                            FirstOrDefaultAsync(u => u.Id == id);
            if (objToGet != null)
            {
                return _mapper.Map<SapToMesMaterialMapping, SapToMesMaterialMappingDTO>(objToGet);
            }
            return null;
        }


        public async Task<IEnumerable<SapToMesMaterialMappingDTO>> GetAll()
        {
            var hhh = _db.SapToMesMaterialMapping.Include("SapMaterial").Include("MesMaterial");            
            return _mapper.Map<IEnumerable<SapToMesMaterialMapping>, IEnumerable<SapToMesMaterialMappingDTO>>(hhh);
            
        }

        public async Task<SapToMesMaterialMappingDTO> Update(SapToMesMaterialMappingDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.SapToMesMaterialMapping.Include("SapMaterial").Include("MesMaterial").
                    FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {

                if (objectToUpdate.SapMaterialId != objectToUpdateDTO.SapMaterialDTO.Id)
                {
                    objectToUpdate.SapMaterialId = objectToUpdateDTO.SapMaterialDTO.Id;                    
                    objectToUpdate.SapMaterial = _mapper.Map<SapMaterialDTO, SapMaterial>(objectToUpdateDTO.SapMaterialDTO);
                }
                if (objectToUpdate.MesMaterialId != objectToUpdateDTO.MesMaterialDTO.Id)
                {
                    objectToUpdate.MesMaterialId = objectToUpdateDTO.MesMaterialDTO.Id;
                    objectToUpdate.MesMaterial = _mapper.Map<MesMaterialDTO, MesMaterial>(objectToUpdateDTO.MesMaterialDTO);
                }
                _db.SapToMesMaterialMapping.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<SapToMesMaterialMapping, SapToMesMaterialMappingDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(int id)
        {
            if (id > 0)
            {
                var objectToDelete = _db.SapToMesMaterialMapping.FirstOrDefault(u => u.Id == id);
                if (objectToDelete != null)
                {
                    _db.SapToMesMaterialMapping.Remove(objectToDelete);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;

        }
    }
}
