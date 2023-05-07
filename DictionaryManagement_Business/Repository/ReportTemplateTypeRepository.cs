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
    public class ReportTemplateTypeRepository : IReportTemplateTypeRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ReportTemplateTypeRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ReportTemplateTypeDTO> Create(ReportTemplateTypeDTO objectToAddDTO)
        {
            var objectToAdd = _mapper.Map<ReportTemplateTypeDTO, ReportTemplateType>(objectToAddDTO);            
            var addedReportTemplateType = _db.ReportTemplateType.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<ReportTemplateType, ReportTemplateTypeDTO>(addedReportTemplateType.Entity);
        }

        public async Task<ReportTemplateTypeDTO> Get(int Id)
        {
            var objToGet = _db.ReportTemplateType.FirstOrDefaultAsync(u => u.Id == Id).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<ReportTemplateType, ReportTemplateTypeDTO>(objToGet);
            }
            return new ReportTemplateTypeDTO();
        }

        public async Task<IEnumerable<ReportTemplateTypeDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All)
        {
            if (selectDictionaryScope == SD.SelectDictionaryScope.All)
            {                
                return _mapper.Map<IEnumerable<ReportTemplateType>, IEnumerable<ReportTemplateTypeDTO>>(_db.ReportTemplateType);
            }
            if (selectDictionaryScope == SD.SelectDictionaryScope.ArchiveOnly)
                return _mapper.Map<IEnumerable<ReportTemplateType>, IEnumerable<ReportTemplateTypeDTO>>(_db.ReportTemplateType.Where(u => u.IsArchive == true));
            if (selectDictionaryScope == SD.SelectDictionaryScope.NotArchiveOnly)
                return _mapper.Map<IEnumerable<ReportTemplateType>, IEnumerable<ReportTemplateTypeDTO>>(_db.ReportTemplateType.Where(u => u.IsArchive != true));
            return _mapper.Map<IEnumerable<ReportTemplateType>, IEnumerable<ReportTemplateTypeDTO>>(_db.ReportTemplateType);
        }

        public async Task<ReportTemplateTypeDTO> Update(ReportTemplateTypeDTO objectToUpdateDTO, UpdateMode updateMode = UpdateMode.Update)
        {
            var objectToUpdate = _db.ReportTemplateType.FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {
                if (updateMode == SD.UpdateMode.Update)
                {
                    if (objectToUpdate.Name != objectToUpdateDTO.Name)
                        objectToUpdate.Name = objectToUpdateDTO.Name;
                }
                if (updateMode == SD.UpdateMode.MoveToArchive)
                {
                    objectToUpdate.IsArchive = true;
                }
                if (updateMode == SD.UpdateMode.RestoreFromArchive)
                {
                    objectToUpdate.IsArchive = false;
                }
                _db.ReportTemplateType.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<ReportTemplateType, ReportTemplateTypeDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }
    }
}
