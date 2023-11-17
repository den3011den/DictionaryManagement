using AutoMapper;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using DND.EFCoreWithNoLock.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository
{
    public class SapMovementsOUTRepository : ISapMovementsOUTRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public SapMovementsOUTRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<SapMovementsOUTDTO> Create(SapMovementsOUTDTO objectToAddDTO)
        {

            SapMovementsOUT objectToAdd = new SapMovementsOUT();

            objectToAdd.Id = objectToAddDTO.Id;

            if (objectToAddDTO.AddTime == null)
                objectToAdd.AddTime = DateTime.Now;
            else
                objectToAdd.AddTime = objectToAddDTO.AddTime;

            objectToAdd.BatchNo = objectToAddDTO.BatchNo;
            objectToAdd.SapMaterialCode = objectToAddDTO.SapMaterialCode;
            objectToAdd.ErpPlantIdSource = objectToAddDTO.ErpPlantIdSource;
            objectToAdd.ErpIdSource = objectToAddDTO.ErpIdSource;
            objectToAdd.IsWarehouseSource = objectToAddDTO.IsWarehouseSource;
            objectToAdd.ErpPlantIdDest = objectToAddDTO.ErpPlantIdDest;
            objectToAdd.ErpIdDest = objectToAddDTO.ErpIdDest;
            objectToAdd.IsWarehouseDest = objectToAddDTO.IsWarehouseDest;            
            objectToAdd.ValueTime = objectToAddDTO.ValueTime;
            objectToAdd.Value = objectToAddDTO.Value;            
            objectToAdd.Correction2Previous = objectToAddDTO.Correction2Previous;
            objectToAdd.MesParamId = objectToAddDTO.MesParamId;
            objectToAdd.IsReconciled = objectToAddDTO.IsReconciled;
            objectToAdd.SapUnitOfMeasure = objectToAddDTO.SapUnitOfMeasure;
            objectToAdd.SapGone = objectToAddDTO.SapGone;
            objectToAdd.SapGoneTime = objectToAddDTO.SapGoneTime;
            objectToAdd.SapError = objectToAddDTO.SapError;
            objectToAdd.SapErrorMessage = objectToAddDTO.SapErrorMessage;
            objectToAdd.MesMovementId = objectToAddDTO.MesMovementId;
            objectToAdd.PreviousRecordId = objectToAddDTO.PreviousRecordId;
            objectToAdd.MesParamId = objectToAddDTO.MesParamId;

            var addedSapMovementsOUT = _db.SapMovementsOUT.Add(objectToAdd);
            _db.SaveChanges();
            return _mapper.Map<SapMovementsOUT, SapMovementsOUTDTO>(addedSapMovementsOUT.Entity);
        }


        public async Task<SapMovementsOUTDTO> GetById(Guid id)
        {
            var objToGet = _db.SapMovementsOUT
                            .Include("MesMovementsFK")
                            .Include("PreviousRecordFK")
                            .Include("MesParamFK")                       
                            .FirstOrDefaultWithNoLock(u => u.Id == id);
            if (objToGet != null)
            {
                return _mapper.Map<SapMovementsOUT, SapMovementsOUTDTO>(objToGet);
            }
            return null;
        }


        public async Task<IEnumerable<SapMovementsOUTDTO>> GetAllByTimeInterval(DateTime? startTime, DateTime? endTime, string intervalMode)
        {

            if (startTime == null)
                startTime = DateTime.MinValue;
            if (startTime == null)
                endTime = DateTime.MaxValue;

            switch (intervalMode.Trim().ToUpper())
            {
                case "ADDTIME":
                    var hhh1 = _db.SapMovementsOUT
                            .Include("MesMovementsFK")
                            .Include("PreviousRecordFK")
                            .Include("MesParamFK")
                        .Where(u => u.AddTime >= startTime && u.AddTime <= endTime).ToListWithNoLock();
                    return _mapper.Map<IEnumerable<SapMovementsOUT>, IEnumerable<SapMovementsOUTDTO> >(hhh1);

                case "VALUETIME":
                    var hhh2 = _db.SapMovementsOUT
                            .Include("MesMovementsFK")
                            .Include("PreviousRecordFK")
                            .Include("MesParamFK")
                        .Where(u => u.ValueTime >= startTime && u.ValueTime <= endTime).ToListWithNoLock();
                    return _mapper.Map<IEnumerable<SapMovementsOUT>, IEnumerable<SapMovementsOUTDTO>>(hhh2);
                default:
                    return null;
            }

        }


        public async Task<SapMovementsOUTDTO> Update(SapMovementsOUTDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.SapMovementsOUT
                            .Include("MesMovementsFK")
                            .Include("PreviousRecordFK")
                            .Include("MesParamFK")
               .FirstOrDefaultWithNoLock(u => u.Id == objectToUpdateDTO.Id);

            if (objectToUpdate != null)
            {
                if (objectToUpdateDTO.MesParamId == null)
                {
                    objectToUpdate.MesParamId = 0;
                    objectToUpdate.MesParamFK = null;
                }
                else
                {
                    if (objectToUpdate.MesParamId != objectToUpdateDTO.MesParamId)
                    {
                        objectToUpdate.MesParamId = objectToUpdateDTO.MesParamId;
                        var objectMesParamToUpdate = _db.MesParam
                            .Include("MesParamSourceTypeFK")
                            .Include("MesDepartmentFK")
                            .Include("SapEquipmentSourceFK")
                            .Include("SapEquipmentDestFK")
                            .Include("MesMaterialFK")
                            .Include("SapMaterialFK")
                            .Include("MesUnitOfMeasureFK")
                            .Include("SapUnitOfMeasureFK")
                            .FirstOrDefaultWithNoLock(u => u.Id == objectToUpdateDTO.MesParamId);
                        objectToUpdate.MesParamFK = objectMesParamToUpdate;
                    }
                }

                if (objectToUpdateDTO.PreviousRecordId == null)
                {
                    objectToUpdate.PreviousRecordId = null;
                    objectToUpdate.PreviousRecordFK = null;
                }
                else
                {
                    if (objectToUpdate.PreviousRecordId != objectToUpdateDTO.PreviousRecordId)
                    {
                        objectToUpdate.PreviousRecordId = objectToUpdateDTO.PreviousRecordId;
                        var objectSapMovementsOUTToUpdate = _db.SapMovementsOUT
                            .Include("MesMovementsFK")
                            .Include("PreviousRecordFK")
                            .Include("MesParamFK")
                            .FirstOrDefaultWithNoLock(u => u.Id == objectToUpdateDTO.PreviousRecordId);
                        objectToUpdate.PreviousRecordFK = objectSapMovementsOUTToUpdate;
                    }
                }

                if (objectToUpdateDTO.MesMovementId == null)
                {
                    objectToUpdate.MesMovementId = null;
                    objectToUpdate.MesMovementsFK = null;
                }
                else
                {
                    if (objectToUpdate.MesMovementId != objectToUpdateDTO.MesMovementId)
                    {
                        objectToUpdate.MesMovementId = objectToUpdateDTO.MesMovementId;
                        var objectMesMovementsToUpdate = _db.MesMovements
                            .Include("MesMovementFK")
                            .Include("PreviousRecordFK")
                            .FirstOrDefaultWithNoLock(u => u.Id == objectToUpdateDTO.MesMovementId);
                        objectToUpdate.MesMovementsFK = objectMesMovementsToUpdate;
                    }
                }

                if (objectToUpdateDTO.AddTime != objectToUpdate.AddTime)
                    objectToUpdate.AddTime = objectToUpdateDTO.AddTime;

                if (objectToUpdateDTO.BatchNo != objectToUpdate.BatchNo)
                    objectToUpdate.BatchNo = objectToUpdateDTO.BatchNo;

                if (objectToUpdateDTO.SapMaterialCode != objectToUpdate.SapMaterialCode)
                    objectToUpdate.SapMaterialCode = objectToUpdateDTO.SapMaterialCode;

                if (objectToUpdateDTO.ErpPlantIdSource != objectToUpdate.ErpPlantIdSource)
                    objectToUpdate.ErpPlantIdSource = objectToUpdateDTO.ErpPlantIdSource;

                if (objectToUpdateDTO.ErpIdSource != objectToUpdate.ErpIdSource)
                    objectToUpdate.ErpIdSource = objectToUpdateDTO.ErpIdSource;

                if (objectToUpdateDTO.IsWarehouseSource != objectToUpdate.IsWarehouseSource)
                    objectToUpdate.IsWarehouseSource = objectToUpdateDTO.IsWarehouseSource;

                if (objectToUpdateDTO.ErpPlantIdDest != objectToUpdate.ErpPlantIdDest)
                    objectToUpdate.ErpPlantIdDest = objectToUpdateDTO.ErpPlantIdDest;

                if (objectToUpdateDTO.ErpIdDest != objectToUpdate.ErpIdDest)
                    objectToUpdate.ErpIdDest = objectToUpdateDTO.ErpIdDest;

                if (objectToUpdateDTO.IsWarehouseDest != objectToUpdate.IsWarehouseDest)
                    objectToUpdate.IsWarehouseDest = objectToUpdateDTO.IsWarehouseDest;

                if (objectToUpdateDTO.ValueTime != objectToUpdate.ValueTime)
                    objectToUpdate.ValueTime = objectToUpdateDTO.ValueTime;

                if (objectToUpdateDTO.Value != objectToUpdate.Value)
                    objectToUpdate.Value = objectToUpdateDTO.Value;

                if (objectToUpdateDTO.Correction2Previous != objectToUpdate.Correction2Previous)
                    objectToUpdate.Correction2Previous = objectToUpdateDTO.Correction2Previous;

                if (objectToUpdateDTO.IsReconciled != objectToUpdate.IsReconciled)
                    objectToUpdate.IsReconciled = objectToUpdateDTO.IsReconciled;

                if (objectToUpdateDTO.SapUnitOfMeasure != objectToUpdate.SapUnitOfMeasure)
                    objectToUpdate.SapUnitOfMeasure = objectToUpdateDTO.SapUnitOfMeasure;

                if (objectToUpdateDTO.SapGone != objectToUpdate.SapGone)
                    objectToUpdate.SapGone = objectToUpdateDTO.SapGone;

                if (objectToUpdateDTO.SapGoneTime != objectToUpdate.SapGoneTime)
                    objectToUpdate.SapGoneTime = objectToUpdateDTO.SapGoneTime;

                if (objectToUpdateDTO.SapError != objectToUpdate.SapError)
                    objectToUpdate.SapError = objectToUpdateDTO.SapError;

                if (objectToUpdateDTO.SapErrorMessage != objectToUpdate.SapErrorMessage)
                    objectToUpdate.SapErrorMessage = objectToUpdateDTO.SapErrorMessage;

                if (objectToUpdateDTO.MesMovementId != objectToUpdate.MesMovementId)
                    objectToUpdate.MesMovementId = objectToUpdateDTO.MesMovementId;

                if (objectToUpdateDTO.PreviousRecordId != objectToUpdate.PreviousRecordId)
                    objectToUpdate.PreviousRecordId = objectToUpdateDTO.PreviousRecordId;


                _db.SapMovementsOUT.Update(objectToUpdate);
                _db.SaveChanges();
                return _mapper.Map<SapMovementsOUT, SapMovementsOUTDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                var objectToDelete = _db.SapMovementsOUT.FirstOrDefaultWithNoLock(u => u.Id == id);
                if (objectToDelete != null)
                {
                    _db.SapMovementsOUT.Remove(objectToDelete);
                    return _db.SaveChanges();
                }
            }
            return 0;
        }
    }
}
