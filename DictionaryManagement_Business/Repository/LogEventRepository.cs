using AutoMapper;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
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

        public LogEventRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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

    }
}

