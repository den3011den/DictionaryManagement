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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository
{
    public class SapMovementsINRepository : ISapMovementsINRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public SapMovementsINRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<SapMovementsINDTO> Create(SapMovementsINDTO objectToAddDTO)
        {

            SapMovementsIN objectToAdd = new SapMovementsIN();

            objectToAdd.ErpId = objectToAddDTO.ErpId;

            if (objectToAddDTO.AddTime == null)
                objectToAdd.AddTime = DateTime.Now;
            else
                objectToAdd.AddTime = objectToAddDTO.AddTime;

            objectToAdd.SapDocumentEnterTime = objectToAddDTO.SapDocumentEnterTime;
            objectToAdd.SapDocumentPostTime = objectToAddDTO.SapDocumentPostTime;
            objectToAdd.BatchNo = objectToAddDTO.BatchNo;
            objectToAdd.SapMaterialCode = objectToAddDTO.SapMaterialCode;
            objectToAdd.ErpPlantIdSource = objectToAddDTO.ErpPlantIdSource;
            objectToAdd.ErpIdSource = objectToAddDTO.ErpIdSource;

            objectToAdd.IsWarehouseSource = objectToAddDTO.IsWarehouseSource;

            objectToAdd.ErpPlantIdDest = objectToAddDTO.ErpPlantIdDest;
            objectToAdd.ErpIdDest = objectToAddDTO.ErpIdDest;
            objectToAdd.IsWarehouseDest = objectToAddDTO.IsWarehouseDest;
            objectToAdd.Value = objectToAddDTO.Value;
            objectToAdd.SapUnitOfMeasure = objectToAddDTO.SapUnitOfMeasure;
            objectToAdd.IsStorno = objectToAddDTO.IsStorno;
            objectToAdd.MesGone = objectToAddDTO.MesGone;

            objectToAdd.MesGoneTime = objectToAddDTO.MesGoneTime;
            objectToAdd.MesError = objectToAddDTO.MesError;
            objectToAdd.MesErrorMessage = objectToAddDTO.MesErrorMessage;
            objectToAdd.MesMovementId = objectToAddDTO.MesMovementId;
            objectToAdd.PreviousErpId = objectToAddDTO.PreviousErpId;
            objectToAdd.MoveType = objectToAddDTO.MoveType;            


            var addedSapMovementsIN = _db.SapMovementsIN.Add(objectToAdd);
            _db.SaveChanges();
            return _mapper.Map<SapMovementsIN, SapMovementsINDTO>(addedSapMovementsIN.Entity);
        }


        public async Task<SapMovementsINDTO> GetById(string erpId)
        {
            var objToGet = _db.SapMovementsIN
                            .Include("MesMovementFK")
                            .Include("PreviousRecordFK")
                            .FirstOrDefaultWithNoLock(u => u.ErpId.Trim().ToUpper() == erpId.Trim().ToUpper());
            if (objToGet != null)
            {
                return _mapper.Map<SapMovementsIN, SapMovementsINDTO>(objToGet);
            }
            return null;
        }


        public async Task<IEnumerable<SapMovementsINDTO>> GetAllByTimeInterval(DateTime? startTime, DateTime? endTime, string intervalMode)
        {

            if (startTime == null)
                startTime = DateTime.MinValue;
            if (startTime == null)
                endTime = DateTime.MaxValue;

            switch (intervalMode.Trim().ToUpper())
            {
                case "ADDTIME":
                    var hhh1 = _db.SapMovementsIN
                        .Include("MesMovementFK")
                        .Include("PreviousRecordFK")
                        .Where(u => u.AddTime >= startTime && u.AddTime <= endTime).ToListWithNoLock();
                    return _mapper.Map<IEnumerable<SapMovementsIN>, IEnumerable<SapMovementsINDTO>>(hhh1);

                case "VALUETIME":
                    var hhh2 = _db.SapMovementsIN
                        .Include("MesMovementFK")
                        .Include("PreviousRecordFK")
                        .Where(u => u.SapDocumentPostTime >= startTime && u.SapDocumentPostTime <= endTime).ToListWithNoLock();
                    return _mapper.Map<IEnumerable<SapMovementsIN>, IEnumerable<SapMovementsINDTO>>(hhh2);
                default:
                    return null;
            }

        }


        public async Task<SapMovementsINDTO> Update(SapMovementsINDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.SapMovementsIN
                        .Include("MesMovementFK")
                        .Include("PreviousRecordFK")
               .FirstOrDefaultWithNoLock(u => u.ErpId.Trim().ToUpper() == objectToUpdateDTO.ErpId.Trim().ToUpper());

            if (objectToUpdate != null)
            {
                if (objectToUpdateDTO.MesMovementId == null)
                {
                    objectToUpdate.MesMovementId = null;
                    objectToUpdate.MesMovementFK = null;
                }
                else
                {
                    if (objectToUpdate.MesMovementId != objectToUpdateDTO.MesMovementId)
                    {
                        objectToUpdate.MesMovementId = objectToUpdateDTO.MesMovementId;
                        var objectMesMovementsToUpdate = _db.MesMovements.
                                FirstOrDefaultWithNoLock(u => u.Id == objectToUpdateDTO.MesMovementId);
                        objectToUpdate.MesMovementFK = objectMesMovementsToUpdate;
                    }
                }

                if (objectToUpdateDTO.PreviousErpId == null)
                {
                    objectToUpdate.PreviousErpId = null;
                    objectToUpdate.PreviousRecordFK = null;
                }
                else
                {
                    if (objectToUpdate.PreviousErpId != objectToUpdateDTO.PreviousErpId)
                    {
                        objectToUpdate.PreviousErpId = objectToUpdateDTO.PreviousErpId;
                        var objectSapMovementsINToUpdate = _db.SapMovementsIN.
                                FirstOrDefaultWithNoLock(u => u.ErpId.Trim().ToUpper() == objectToUpdateDTO.PreviousErpId.Trim().ToUpper());
                        objectToUpdate.PreviousRecordFK = objectSapMovementsINToUpdate;
                    }
                }


                if (objectToUpdateDTO.AddTime != objectToUpdate.AddTime)
                    objectToUpdate.AddTime = objectToUpdateDTO.AddTime;

                if (objectToUpdateDTO.SapDocumentEnterTime != objectToUpdate.SapDocumentEnterTime)
                    objectToUpdate.SapDocumentEnterTime = objectToUpdateDTO.SapDocumentEnterTime;

                if (objectToUpdateDTO.SapDocumentPostTime != objectToUpdate.SapDocumentPostTime)
                    objectToUpdate.SapDocumentPostTime = objectToUpdateDTO.SapDocumentPostTime;

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

                if (objectToUpdateDTO.Value != objectToUpdate.Value)
                    objectToUpdate.Value = objectToUpdateDTO.Value;

                if (objectToUpdateDTO.SapUnitOfMeasure != objectToUpdate.SapUnitOfMeasure)
                    objectToUpdate.SapUnitOfMeasure = objectToUpdateDTO.SapUnitOfMeasure;

                if (objectToUpdateDTO.IsStorno != objectToUpdate.IsStorno)
                    objectToUpdate.IsStorno = objectToUpdateDTO.IsStorno;

                if (objectToUpdateDTO.MesGone != objectToUpdate.MesGone)
                    objectToUpdate.MesGone = objectToUpdateDTO.MesGone;

                if (objectToUpdateDTO.MesGoneTime != objectToUpdate.MesGoneTime)
                    objectToUpdate.MesGoneTime = objectToUpdateDTO.MesGoneTime;

                if (objectToUpdateDTO.MesError != objectToUpdate.MesError)
                    objectToUpdate.MesError = objectToUpdateDTO.MesError;

                if (objectToUpdateDTO.MesErrorMessage != objectToUpdate.MesErrorMessage)
                    objectToUpdate.MesErrorMessage = objectToUpdateDTO.MesErrorMessage;

                if (objectToUpdateDTO.MoveType != objectToUpdate.MoveType)
                    objectToUpdate.MoveType = objectToUpdateDTO.MoveType;

                _db.SapMovementsIN.Update(objectToUpdate);
                _db.SaveChanges();
                return _mapper.Map<SapMovementsIN, SapMovementsINDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(string id)
        {
            if (id.Trim().IsNullOrEmpty())
            {
                var objectToDelete = _db.SapMovementsIN.FirstOrDefaultWithNoLock(u => u.ErpId.Trim().ToUpper() == id.Trim().ToUpper());
                if (objectToDelete != null)
                {
                    _db.SapMovementsIN.Remove(objectToDelete);
                    return _db.SaveChanges();
                }
            }
            return 0;
        }
    }
}
