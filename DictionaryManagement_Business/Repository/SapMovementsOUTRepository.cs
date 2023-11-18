using AutoMapper;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using DND.EFCoreWithNoLock.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
                            //.Include("SapEquipmentSourceFK")
                            //.Include("SapEquipmentDestFK")
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

            IEnumerable<SapMovementsOUT> hhh2;
            switch (intervalMode.Trim().ToUpper())
            {
                case "ADDTIME":
                    hhh2 = _db.SapMovementsOUT
                            .Include("MesMovementsFK")
                            .Include("PreviousRecordFK")
                            .Include("MesParamFK")
                        //.Include("MesParamFK")
                        //.Include("MesParamFK")
                        .Where(u => u.AddTime >= startTime && u.AddTime <= endTime).ToListWithNoLock();
                    break;
                case "VALUETIME":
                    //hhh2 = _db.SapMovementsOUT
                    //        .Include("MesMovementsFK")
                    //        .Include("PreviousRecordFK")
                    //        .Include("MesParamFK")
                    //        //.Include("MesParamFK")
                    //        //.Include("MesParamFK")
                    //    .Where(u => u.ValueTime >= startTime && u.ValueTime <= endTime).ToListWithNoLock();

                    //hhh2 = from school in ctx.Schools
                    //join teacher in ctx.Teachers on school.TeacherId equals teacher.Id into grouping
                    //from t in grouping.DefaultIfEmpty()
                    //join student in ctx.Students on school.StudentId equals student.Id into grouping2
                    //from s in grouping2.DefaultIfEmpty()
                    //where school.Id == someSchoolId
                    //select school;


                    hhh2 = (from SMOUT in _db.SapMovementsOUT
                                    .Include("MesMovementsFK")
                                    .Include("PreviousRecordFK")
                                    .Include("MesParamFK").Where(u => u.ValueTime >= startTime && u.ValueTime <= endTime).ToListWithNoLock()
                            join SM in _db.SapMaterial.DefaultIfEmpty().ToListWithNoLock() on SMOUT.SapMaterialCode equals SM.Code
                           select
                            (new SapMovementsOUT
                                {
                                    Id = SMOUT.Id,
                                    AddTime = SMOUT.AddTime,
                                    BatchNo = SMOUT.BatchNo,
                                    SapMaterialCode = SMOUT.SapMaterialCode,
                                    SapMaterialFK = SM,
                                    ErpPlantIdSource = SMOUT.ErpPlantIdSource,
                                    ErpIdSource = SMOUT.ErpIdSource,
                                    IsWarehouseSource = SMOUT.IsWarehouseSource,
                                    ErpPlantIdDest = SMOUT.ErpPlantIdDest,
                                    ErpIdDest = SMOUT.ErpIdDest,
                                    IsWarehouseDest = SMOUT.IsWarehouseDest,
                                    ValueTime = SMOUT.ValueTime,
                                    Value = SMOUT.Value,
                                    Correction2Previous = SMOUT.Correction2Previous,
                                    IsReconciled = SMOUT.IsReconciled,
                                    SapUnitOfMeasure = SMOUT.SapUnitOfMeasure,
                                    SapGone = SMOUT.SapGone,
                                    SapGoneTime = SMOUT.SapGoneTime,
                                    SapError = SMOUT.SapError,
                                    SapErrorMessage = SMOUT.SapErrorMessage,
                                    MesMovementId = SMOUT.MesMovementId,
                                    MesMovementsFK = SMOUT.MesMovementsFK,
                                    MesParamId = SMOUT.MesParamId,
                                    MesParamFK = SMOUT.MesParamFK,
                                    PreviousRecordFK = SMOUT.PreviousRecordFK})).ToList();

                    //hhh2 = _db.SapMovementsOUT
                    //    .Include("MesMovementsFK")
                    //    .Include("PreviousRecordFK")
                    //    .Include("MesParamFK")
                    //    .Join(_db.SapMaterial,
                    //        u => u.SapMaterialCode,
                    //        c => c.Code,
                    //    (u, c) => new SapMovementsOUT
                    //    {
                    //        Id = u.Id,
                    //        AddTime = u.AddTime,
                    //        BatchNo = u.BatchNo,
                    //        SapMaterialCode = u.SapMaterialCode,
                    //        SapMaterialFK = c,
                    //        ErpPlantIdSource = u.ErpPlantIdSource,
                    //        ErpIdSource = u.ErpIdSource,
                    //        IsWarehouseSource = u.IsWarehouseSource,
                    //        ErpPlantIdDest = u.ErpPlantIdDest,
                    //        ErpIdDest = u.ErpIdDest,
                    //        IsWarehouseDest = u.IsWarehouseDest,
                    //        ValueTime = u.ValueTime,
                    //        Value = u.Value,
                    //        Correction2Previous = u.Correction2Previous,
                    //        IsReconciled = u.IsReconciled,
                    //        SapUnitOfMeasure = u.SapUnitOfMeasure,
                    //        SapGone = u.SapGone,
                    //        SapGoneTime = u.SapGoneTime,
                    //        SapError = u.SapError,
                    //        SapErrorMessage = u.SapErrorMessage,
                    //        MesMovementId = u.MesMovementId,
                    //        MesMovementsFK = u.MesMovementsFK,
                    //        MesParamId = u.MesParamId,
                    //        MesParamFK = u.MesParamFK,
                    //        PreviousRecordFK = u.PreviousRecordFK
                    //    }).Where(u => u.ValueTime >= startTime && u.ValueTime <= endTime).ToListWithNoLock();

                    break;
                default:
                    return null;

            }
            var a = 1;
            var hhh3 = _mapper.Map<IEnumerable<SapMovementsOUT>, IEnumerable<SapMovementsOUTDTO>>(hhh2);
            return hhh3;

            //var hhh3 = _mapper.Map<IEnumerable<SapMovementsOUT>, IEnumerable<SapMovementsOUTDTO>>(hhh2);
            //foreach (var element in hhh3)
            //{
            //    if (!element.ErpIdSource.IsNullOrEmpty() && !element.ErpPlantIdSource.IsNullOrEmpty())
            //    {
            //        var sapSource = _db.SapEquipment.FirstOrDefaultWithNoLock(u => u.ErpPlantId == element.ErpPlantIdSource && u.ErpId == element.ErpIdSource);
            //        element.SapEquipmentSourceDTOFK = _mapper.Map<SapEquipment, SapEquipmentDTO>(sapSource);
            //    }
            //    if (!element.ErpIdDest.IsNullOrEmpty() && !element.ErpPlantIdDest.IsNullOrEmpty())
            //    {
            //        var sapDest = _db.SapEquipment.FirstOrDefaultWithNoLock(u => u.ErpPlantId == element.ErpPlantIdDest && u.ErpId == element.ErpIdDest);
            //        element.SapEquipmentDestDTOFK = _mapper.Map<SapEquipment, SapEquipmentDTO>(sapDest);
            //    }
            //    if (!element.SapMaterialCode.IsNullOrEmpty())
            //    {
            //        var sapMaterial = _db.SapMaterial.FirstOrDefaultWithNoLock(u => u.Code == element.SapMaterialCode);
            //        element.SapMaterialDTOFK = _mapper.Map<SapMaterial, SapMaterialDTO>(sapMaterial);
            //    }
            //}
            //return hhh3;
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
