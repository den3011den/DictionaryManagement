﻿using AutoMapper;
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
    public class SapUnitOfMeasureRepository : ISapUnitOfMeasureRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public SapUnitOfMeasureRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<SapUnitOfMeasureDTO> Create(SapUnitOfMeasureDTO objectToAddDTO)
        {
            var objectToAdd = _mapper.Map<SapUnitOfMeasureDTO, SapUnitOfMeasure>(objectToAddDTO);            
            var addedSapUnitOfMeasure = _db.SapUnitOfMeasure.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<SapUnitOfMeasure, SapUnitOfMeasureDTO>(addedSapUnitOfMeasure.Entity);
        }

        public async Task<SapUnitOfMeasureDTO> Get(int Id)
        {
            var objToGet = _db.SapUnitOfMeasure.FirstOrDefaultAsync(u => u.Id == Id).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<SapUnitOfMeasure, SapUnitOfMeasureDTO>(objToGet);
            }
            return new SapUnitOfMeasureDTO();
        }

        public async Task<IEnumerable<SapUnitOfMeasureDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All)
        {
            if (selectDictionaryScope == SD.SelectDictionaryScope.All)
            {                
                return _mapper.Map<IEnumerable<SapUnitOfMeasure>, IEnumerable<SapUnitOfMeasureDTO>>(_db.SapUnitOfMeasure);
            }
            if (selectDictionaryScope == SD.SelectDictionaryScope.ArchiveOnly)
                return _mapper.Map<IEnumerable<SapUnitOfMeasure>, IEnumerable<SapUnitOfMeasureDTO> >(_db.SapUnitOfMeasure.Where(u => u.IsArchive == true));
            if (selectDictionaryScope == SD.SelectDictionaryScope.NotArchiveOnly)
                return _mapper.Map<IEnumerable<SapUnitOfMeasure>, IEnumerable<SapUnitOfMeasureDTO>>(_db.SapUnitOfMeasure.Where(u => u.IsArchive != true));
            return _mapper.Map<IEnumerable<SapUnitOfMeasure>, IEnumerable<SapUnitOfMeasureDTO>>(_db.SapUnitOfMeasure);
        }

        public async Task<SapUnitOfMeasureDTO> Update(SapUnitOfMeasureDTO objectToUpdateDTO, UpdateMode updateMode = UpdateMode.Update)
        {
            var objectToUpdate = _db.SapUnitOfMeasure.FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {
                if (updateMode == SD.UpdateMode.Update)
                {
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
                _db.SapUnitOfMeasure.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<SapUnitOfMeasure, SapUnitOfMeasureDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }
    }
}
