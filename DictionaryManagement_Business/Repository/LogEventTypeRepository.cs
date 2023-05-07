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
    public class LogEventTypeRepository : ILogEventTypeRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public LogEventTypeRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<LogEventTypeDTO> Create(LogEventTypeDTO objectToAddDTO)
        {
            var objectToAdd = _mapper.Map<LogEventTypeDTO, LogEventType>(objectToAddDTO);            
            var addedLogEventType = _db.LogEventType.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<LogEventType, LogEventTypeDTO>(addedLogEventType.Entity);
        }

        public async Task<LogEventTypeDTO> Get(int Id)
        {
            var objToGet = _db.LogEventType.FirstOrDefaultAsync(u => u.Id == Id).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<LogEventType, LogEventTypeDTO>(objToGet);
            }
            return new LogEventTypeDTO();
        }

        public async Task<IEnumerable<LogEventTypeDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All)
        {
            if (selectDictionaryScope == SD.SelectDictionaryScope.All)
            {                
                return _mapper.Map<IEnumerable<LogEventType>, IEnumerable<LogEventTypeDTO>>(_db.LogEventType);
            }
            if (selectDictionaryScope == SD.SelectDictionaryScope.ArchiveOnly)
                return _mapper.Map<IEnumerable<LogEventType>, IEnumerable<LogEventTypeDTO>>(_db.LogEventType.Where(u => u.IsArchive == true));
            if (selectDictionaryScope == SD.SelectDictionaryScope.NotArchiveOnly)
                return _mapper.Map<IEnumerable<LogEventType>, IEnumerable<LogEventTypeDTO>>(_db.LogEventType.Where(u => u.IsArchive != true));
            return _mapper.Map<IEnumerable<LogEventType>, IEnumerable<LogEventTypeDTO>>(_db.LogEventType);
        }

        public async Task<LogEventTypeDTO> Update(LogEventTypeDTO objectToUpdateDTO, UpdateMode updateMode = UpdateMode.Update)
        {
            var objectToUpdate = _db.LogEventType.FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
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
                _db.LogEventType.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<LogEventType, LogEventTypeDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }
    }
}
