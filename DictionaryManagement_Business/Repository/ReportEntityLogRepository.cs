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
    public class ReportEntityLogRepository : IReportEntityLogRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ReportEntityLogRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ReportEntityLogDTO> Create(ReportEntityLogDTO objectToAddDTO)
        {
            ReportEntityLog objectToAdd = new ReportEntityLog();

           objectToAdd.Id = objectToAddDTO.Id;

            if (objectToAddDTO.LogTime == null)
                objectToAdd.LogTime = DateTime.Now;
            else
                objectToAdd.LogTime = objectToAddDTO.LogTime;

            objectToAdd.ReportEntityId = objectToAddDTO.ReportEntityId;            
            objectToAdd.LogMessage = objectToAddDTO.LogMessage;
            objectToAdd.LogType = objectToAddDTO.LogType;
            objectToAdd.IsError = objectToAddDTO.IsError;

            var addedReportEntityLog = _db.ReportEntityLog.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<ReportEntityLog, ReportEntityLogDTO>(addedReportEntityLog.Entity);
        }


        public async Task<ReportEntityLogDTO> GetById(Int64 id)
        {
            var objToGet = _db.ReportEntityLog
                            .Include("ReportEntityFK")
                            .FirstOrDefaultAsync(u => u.Id == id).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<ReportEntityLog, ReportEntityLogDTO>(objToGet);
            }
            return null;
        }


        public async Task<IEnumerable<ReportEntityLogDTO>> GetAll()
        {
            var hhh1 = _db.ReportEntityLog
                            .Include("ReportEntityFK");
            return _mapper.Map<IEnumerable<ReportEntityLog>, IEnumerable<ReportEntityLogDTO>>(hhh1);
        }

        public async Task<IEnumerable<ReportEntityLogDTO>> GetAllByLogTimeInterval(DateTime startLogTime, DateTime endLogTime)
        {
            var hhh1 =  _db.ReportEntityLog
                            .Include("ReportEntityFK")
                            .Where(u => u.LogTime >= startLogTime && u.LogTime <= endLogTime);
            return _mapper.Map<IEnumerable<ReportEntityLog>, IEnumerable<ReportEntityLogDTO>>(hhh1);

        }

        public async Task<ReportEntityLogDTO> Update(ReportEntityLogDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.ReportEntityLog
                .Include("ReportEntityFK")
               .FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);

            if (objectToUpdate != null)
            {
                if (objectToUpdateDTO.ReportEntityId == null || objectToUpdateDTO.ReportEntityId == Guid.Empty)
                {
                    objectToUpdate.ReportEntityId = Guid.Empty;
                    objectToUpdate.ReportEntityFK = null;
                }
                else
                {
                    if (objectToUpdate.ReportEntityId != objectToUpdateDTO.ReportEntityId)
                    {
                        objectToUpdate.ReportEntityId = objectToUpdateDTO.ReportEntityId;
                        var objectReportEntityToUpdate = _db.ReportEntity.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.ReportEntityId);
                        objectToUpdate.ReportEntityFK = objectReportEntityToUpdate;
                    }
                }

                if (objectToUpdateDTO.LogTime != objectToUpdate.LogTime)
                    objectToUpdate.LogTime = objectToUpdateDTO.LogTime;

                if (objectToUpdateDTO.LogMessage != objectToUpdate.LogMessage)
                    objectToUpdate.LogMessage = objectToUpdateDTO.LogMessage;

                if (objectToUpdateDTO.LogType != objectToUpdate.LogType)
                    objectToUpdate.LogType = objectToUpdateDTO.LogType;

                if (objectToUpdateDTO.IsError != objectToUpdate.IsError)
                    objectToUpdate.IsError = objectToUpdateDTO.IsError;

                _db.ReportEntityLog.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<ReportEntityLog, ReportEntityLogDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(Int64 id)
        {
            if (id > 0)
            {
                var objectToDelete = _db.ReportEntityLog.FirstOrDefault(u => u.Id == id);
                if (objectToDelete != null)
                {
                    _db.ReportEntityLog.Remove(objectToDelete);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;

        }
    }
}
