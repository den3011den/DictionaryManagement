using AutoMapper;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using DND.EFCoreWithNoLock.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DictionaryManagement_Business.Repository
{
    public class LogEventRepository : ILogEventRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogEventTypeRepository _logEventTypeRepository;

        public LogEventRepository(IntDBApplicationDbContext db, IMapper mapper, ILogEventTypeRepository logEventTypeRepository)
        {
            _db = db;
            _mapper = mapper;
            _logEventTypeRepository = logEventTypeRepository;
        }

        public async Task<IEnumerable<LogEventDTO>> GetAllByTimeInterval(DateTime? startTime, DateTime? endTime)
        {

            if (startTime == null)
                startTime = DateTime.MinValue;
            if (startTime == null)
                endTime = DateTime.MaxValue;

            var hhh2 = _db.LogEvent
                        .Include("LogEventTypeFK")
                        .Include("UserFK")
                        .Where(u => u.EventTime >= startTime && u.EventTime <= endTime).ToListWithNoLock();
            return _mapper.Map<IEnumerable<LogEvent>, IEnumerable<LogEventDTO>>(hhh2);
        }

        public async Task<LogEventDTO?> AddRecord(string logEventTypeName, Guid userId, string oldValue, string newValue, bool isError, string description)
        {
            var logEventTypeDTO = await _logEventTypeRepository.GetByName(logEventTypeName);
            int logEventTypeId = 1;
            if (logEventTypeDTO != null)
                logEventTypeId = logEventTypeDTO.Id;

            if (logEventTypeId != null)
            {
                var objectToAdd = new LogEvent
                {
                    LogEventTypeId = logEventTypeId,
                    UserId = userId,
                    Description = description,
                    IsError = isError,
                    IsCritical = false,
                    IsWarning = false,
                    OldValue = oldValue,
                    NewValue = newValue,
                    EventTime = DateTime.Now
                };

                var addedLogEvent = _db.LogEvent.Add(objectToAdd);
                _db.SaveChanges();
                return _mapper.Map<LogEvent, LogEventDTO>(addedLogEvent.Entity);
            }
            else
                return null;
        }

    }
}

