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
    public class ErrorCriterionRepository : IErrorCriterionRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ErrorCriterionRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ErrorCriterionDTO> Create(ErrorCriterionDTO objectToAddDTO)
        {
            var objectToAdd = _mapper.Map<ErrorCriterionDTO, ErrorCriterion>(objectToAddDTO);            
            var addedErrorCriterion = _db.ErrorCriterion.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<ErrorCriterion, ErrorCriterionDTO>(addedErrorCriterion.Entity);
        }

        public async Task<ErrorCriterionDTO> Get(int Id)
        {
            var objToGet = _db.ErrorCriterion.FirstOrDefaultAsync(u => u.Id == Id).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<ErrorCriterion, ErrorCriterionDTO>(objToGet);
            }
            return new ErrorCriterionDTO();
        }

        public async Task<IEnumerable<ErrorCriterionDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All)
        {
            if (selectDictionaryScope == SD.SelectDictionaryScope.All)
            {
                return _mapper.Map<IEnumerable<ErrorCriterion>, IEnumerable<ErrorCriterionDTO>>(_db.ErrorCriterion);
            }
            if (selectDictionaryScope == SD.SelectDictionaryScope.ArchiveOnly)
                return _mapper.Map<IEnumerable<ErrorCriterion>, IEnumerable<ErrorCriterionDTO>>(_db.ErrorCriterion.Where(u => u.IsArchive == true));
            if (selectDictionaryScope == SD.SelectDictionaryScope.NotArchiveOnly)
                return _mapper.Map<IEnumerable<ErrorCriterion>, IEnumerable<ErrorCriterionDTO>>(_db.ErrorCriterion.Where(u => u.IsArchive != true));
            return _mapper.Map<IEnumerable<ErrorCriterion>, IEnumerable<ErrorCriterionDTO>>(_db.ErrorCriterion);
        }

        public async Task<ErrorCriterionDTO> Update(ErrorCriterionDTO objectToUpdateDTO, UpdateMode updateMode = UpdateMode.Update)
        {
            var objectToUpdate = _db.ErrorCriterion.FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
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
                _db.ErrorCriterion.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<ErrorCriterion, ErrorCriterionDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }
    }
}
