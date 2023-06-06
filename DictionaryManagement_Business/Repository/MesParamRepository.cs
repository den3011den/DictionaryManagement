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
    public class MesParamRepository : IMesParamRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public MesParamRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<MesParamDTO> Create(MesParamDTO objectToAddDTO)
        {
            //var objectToAdd = _mapper.Map<UnitOfMeasureSapToMesMappingDTO, UnitOfMeasureSapToMesMapping>(objectToAddDTO);

            MesParam objectToAdd = new MesParam();

            objectToAdd.Id = objectToAddDTO.Id;
            objectToAdd.Code = objectToAddDTO.Code;
            objectToAdd.Name = objectToAddDTO.Name;
            objectToAdd.Description = objectToAddDTO.Description;
            objectToAdd.MesParamSourceType = objectToAddDTO.MesParamSourceType;
            objectToAdd.MesParamSourceLink = objectToAddDTO.MesParamSourceLink;
            objectToAdd.DepartmentId = objectToAddDTO.DepartmentId;
            objectToAdd.SapEquipmentIdSource = objectToAddDTO.SapEquipmentIdSource;
            objectToAdd.SapEquipmentIdDest = objectToAddDTO.SapEquipmentIdDest;
            objectToAdd.MesMaterialId = objectToAddDTO.MesMaterialId;
            objectToAdd.SapMaterialId = objectToAddDTO.SapMaterialId;
            objectToAdd.MesUnitOfMeasureId = objectToAddDTO.MesUnitOfMeasureId;
            objectToAdd.SapUnitOfMeasureId = objectToAddDTO.SapUnitOfMeasureId;
            objectToAdd.DaysRequestInPast = objectToAddDTO.DaysRequestInPast;
            objectToAdd.NeedWriteToSap = objectToAddDTO.NeedWriteToSap;
            objectToAdd.NeedReadFromSap = objectToAddDTO.NeedReadFromSap;
            objectToAdd.NeedReadFromMes = objectToAddDTO.NeedReadFromMes;
            objectToAdd.NeedWriteToMes = objectToAddDTO.NeedWriteToMes;
            objectToAdd.IsNdo = objectToAddDTO.IsNdo;
            objectToAdd.IsArchive = objectToAddDTO.IsArchive;

            var addedMesParam = _db.MesParam.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<MesParam, MesParamDTO>(addedMesParam.Entity);
        }


        public async Task<MesParamDTO> GetById(int id)
        {
            var objToGet = _db.MesParam
                            .Include("MesParamSourceTypeFK")
                            .Include("MesDepartmentFK")
                            .Include("SapEquipmentSourceFK")
                            .Include("SapEquipmentDestFK")
                            .Include("MesMaterialFK")
                            .Include("SapMaterialFK")
                            .Include("MesUnitOfMeasureFK")
                            .Include("SapUnitOfMeasureFK")
                            .FirstOrDefaultAsync(u => u.Id == id).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<MesParam, MesParamDTO>(objToGet);
            }
            return null;
        }


        public async Task<MesParamDTO> GetByCode(string code = "")
        {
            var objToGet = _db.MesParam
                            .Include("MesParamSourceTypeFK")
                            .Include("MesDepartmentFK")
                            .Include("SapEquipmentSourceFK")
                            .Include("SapEquipmentDestFK")
                            .Include("MesMaterialFK")
                            .Include("SapMaterialFK")
                            .Include("MesUnitOfMeasureFK")
                            .Include("SapUnitOfMeasureFK")
                            .FirstOrDefaultAsync(u => u.Code.Trim().ToUpper() == code.Trim().ToUpper()).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<MesParam, MesParamDTO>(objToGet);
            }
            return null;
        }

        public async Task<MesParamDTO> GetByName(string name = "")
        {
            var objToGet = _db.MesParam
                            .Include("MesParamSourceTypeFK")
                            .Include("MesDepartmentFK")
                            .Include("SapEquipmentSourceFK")
                            .Include("SapEquipmentDestFK")
                            .Include("MesMaterialFK")
                            .Include("SapMaterialFK")
                            .Include("MesUnitOfMeasureFK")
                            .Include("SapUnitOfMeasureFK")
                            .FirstOrDefaultAsync(u => u.Name.Trim().ToUpper() == name.Trim().ToUpper()).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<MesParam, MesParamDTO>(objToGet);
            }
            return null;
        }
        public async Task<MesParamDTO> GetByMesParamSourceLink(string mesParamSourceLink = "")
        {
            var objToGet = _db.MesParam
                            .Include("MesParamSourceTypeFK")
                            .Include("MesDepartmentFK")
                            .Include("SapEquipmentSourceFK")
                            .Include("SapEquipmentDestFK")
                            .Include("MesMaterialFK")
                            .Include("SapMaterialFK")
                            .Include("MesUnitOfMeasureFK")
                            .Include("SapUnitOfMeasureFK")
                            .FirstOrDefaultAsync(u => u.MesParamSourceLink.Trim().ToUpper() == mesParamSourceLink.Trim().ToUpper()).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<MesParam, MesParamDTO>(objToGet);
            }
            return null;
        }


        public async Task<IEnumerable<MesParamDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All)
        {
            if (selectDictionaryScope == SD.SelectDictionaryScope.ArchiveOnly)
            {
                var hhh2 = _db.MesParam
                            .Include("MesParamSourceTypeFK")
                            .Include("MesDepartmentFK")
                            .Include("SapEquipmentSourceFK")
                            .Include("SapEquipmentDestFK")
                            .Include("MesMaterialFK")
                            .Include("SapMaterialFK")
                            .Include("MesUnitOfMeasureFK")
                            .Include("SapUnitOfMeasureFK")
                            .Where(u => u.IsArchive == true);
                return _mapper.Map<IEnumerable<MesParam>, IEnumerable<MesParamDTO>>(hhh2);
            }
            if (selectDictionaryScope == SD.SelectDictionaryScope.NotArchiveOnly)
            {
                var hhh3 = _db.MesParam
                            .Include("MesParamSourceTypeFK")
                            .Include("MesDepartmentFK")
                            .Include("SapEquipmentSourceFK")
                            .Include("SapEquipmentDestFK")
                            .Include("MesMaterialFK")
                            .Include("SapMaterialFK")
                            .Include("MesUnitOfMeasureFK")
                            .Include("SapUnitOfMeasureFK")
                            .Where(u => u.IsArchive != true);
                return _mapper.Map<IEnumerable<MesParam>, IEnumerable<MesParamDTO>>(hhh3);

            }
            var hhh1 = _db.MesParam
                        .Include("MesParamSourceTypeFK")
                        .Include("MesDepartmentFK")
                        .Include("SapEquipmentSourceFK")
                        .Include("SapEquipmentDestFK")
                        .Include("MesMaterialFK")
                        .Include("SapMaterialFK")
                        .Include("MesUnitOfMeasureFK")
                        .Include("SapUnitOfMeasureFK");
            return _mapper.Map<IEnumerable<MesParam>, IEnumerable<MesParamDTO>>(hhh1);
        }


        public async Task<MesParamDTO> Update(MesParamDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.MesParam
                        .Include("MesParamSourceTypeFK")
                        .Include("MesDepartmentFK")
                        .Include("SapEquipmentSourceFK")
                        .Include("SapEquipmentDestFK")
                        .Include("MesMaterialFK")
                        .Include("SapMaterialFK")
                        .Include("MesUnitOfMeasureFK")
                        .Include("SapUnitOfMeasureFK")
                        .FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);

            if (objectToUpdate != null)
            {
                if (objectToUpdateDTO.MesParamSourceType == null)
                {
                    objectToUpdate.MesParamSourceType = null;
                    objectToUpdate.MesParamSourceTypeFK = null;
                }
                else
                {
                    if (objectToUpdate.MesParamSourceType != objectToUpdateDTO.MesParamSourceType)
                    {
                        objectToUpdate.MesParamSourceType = objectToUpdateDTO.MesParamSourceType;
                        var objectMesParamSourceTypeToUpdate = _db.MesParamSourceType.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.MesParamSourceType);
                        objectToUpdate.MesParamSourceTypeFK = objectMesParamSourceTypeToUpdate;
                    }
                }

                if (objectToUpdateDTO.DepartmentId == null)
                {
                    objectToUpdate.DepartmentId = null;
                    objectToUpdate.MesDepartmentFK = null;
                }
                else
                {
                    if (objectToUpdate.DepartmentId != objectToUpdateDTO.DepartmentId)
                    {
                        objectToUpdate.DepartmentId = objectToUpdateDTO.DepartmentId;
                        var objectMesDepartmentToUpdate = _db.MesDepartment.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.DepartmentId);
                        objectToUpdate.MesDepartmentFK = objectMesDepartmentToUpdate;
                    }
                }

                if (objectToUpdateDTO.SapEquipmentIdSource == null)
                {
                    objectToUpdate.SapEquipmentIdSource = null;
                    objectToUpdate.SapEquipmentSourceFK = null;
                }
                else
                {
                    if (objectToUpdate.SapEquipmentIdSource != objectToUpdateDTO.SapEquipmentIdSource)
                    {
                        objectToUpdate.SapEquipmentIdSource = objectToUpdateDTO.SapEquipmentIdSource;
                        var objectSapEquipmentToUpdate = _db.SapEquipment.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.SapEquipmentIdSource);
                        objectToUpdate.SapEquipmentSourceFK = objectSapEquipmentToUpdate;
                    }
                }

                if (objectToUpdateDTO.SapEquipmentIdDest == null)
                {
                    objectToUpdate.SapEquipmentIdDest = null;
                    objectToUpdate.SapEquipmentDestFK = null;
                }
                else
                {
                    if (objectToUpdate.SapEquipmentIdDest != objectToUpdateDTO.SapEquipmentIdDest)
                    {
                        objectToUpdate.SapEquipmentIdDest = objectToUpdateDTO.SapEquipmentIdDest;
                        var objectSapEquipmentToUpdate = _db.SapEquipment.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.SapEquipmentIdDest);
                        objectToUpdate.SapEquipmentDestFK = objectSapEquipmentToUpdate;
                    }
                }


                if (objectToUpdateDTO.MesMaterialId == null)
                {
                    objectToUpdate.MesMaterialId = null;
                    objectToUpdate.MesMaterialFK = null;
                }
                else
                {
                    if (objectToUpdate.MesMaterialId != objectToUpdateDTO.MesMaterialId)
                    {
                        objectToUpdate.MesMaterialId = objectToUpdateDTO.MesMaterialId;
                        var objectMesMaterialToUpdate = _db.MesMaterial.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.MesMaterialId);
                        objectToUpdate.MesMaterialFK = objectMesMaterialToUpdate;
                    }
                }


                if (objectToUpdateDTO.SapMaterialId == null)
                {
                    objectToUpdate.SapMaterialId = null;
                    objectToUpdate.SapMaterialFK = null;
                }
                else
                {
                    if (objectToUpdate.SapMaterialId != objectToUpdateDTO.SapMaterialId)
                    {
                        objectToUpdate.SapMaterialId = objectToUpdateDTO.SapMaterialId;
                        var objectSapMaterialToUpdate = _db.SapMaterial.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.SapMaterialId);
                        objectToUpdate.SapMaterialFK = objectSapMaterialToUpdate;
                    }
                }

                if (objectToUpdateDTO.MesUnitOfMeasureId == null)
                {
                    objectToUpdate.MesUnitOfMeasureId = null;
                    objectToUpdate.MesUnitOfMeasureFK = null;
                }
                else
                {
                    if (objectToUpdate.MesUnitOfMeasureId != objectToUpdateDTO.MesUnitOfMeasureId)
                    {
                        objectToUpdate.MesUnitOfMeasureId = objectToUpdateDTO.MesUnitOfMeasureId;
                        var objectMesUnitOfMeasureToUpdate = _db.MesUnitOfMeasure.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.MesUnitOfMeasureId);
                        objectToUpdate.MesUnitOfMeasureFK = objectMesUnitOfMeasureToUpdate;
                    }
                }

                if (objectToUpdateDTO.SapUnitOfMeasureId == null)
                {
                    objectToUpdate.SapUnitOfMeasureId = null;
                    objectToUpdate.SapUnitOfMeasureFK = null;
                }
                else
                {
                    if (objectToUpdate.SapUnitOfMeasureId != objectToUpdateDTO.SapUnitOfMeasureId)
                    {
                        objectToUpdate.SapUnitOfMeasureId = objectToUpdateDTO.SapUnitOfMeasureId;
                        var objectSapUnitOfMeasureToUpdate = _db.SapUnitOfMeasure.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.SapUnitOfMeasureId);
                        objectToUpdate.SapUnitOfMeasureFK = objectSapUnitOfMeasureToUpdate;
                    }
                }

                if (objectToUpdateDTO.Code != objectToUpdate.Code)
                    objectToUpdate.Code = objectToUpdateDTO.Code;
                if (objectToUpdateDTO.Name != objectToUpdate.Name)
                    objectToUpdate.Name = objectToUpdateDTO.Name;
                if (objectToUpdateDTO.Description != objectToUpdate.Description)
                    objectToUpdate.Description = objectToUpdateDTO.Description;
                if (objectToUpdateDTO.MesParamSourceLink != objectToUpdate.MesParamSourceLink)
                    objectToUpdate.MesParamSourceLink = objectToUpdateDTO.MesParamSourceLink;
                if (objectToUpdateDTO.DaysRequestInPast != objectToUpdate.DaysRequestInPast)
                    objectToUpdate.DaysRequestInPast = objectToUpdateDTO.DaysRequestInPast;
                if (objectToUpdateDTO.NeedWriteToSap != objectToUpdate.NeedWriteToSap)
                    objectToUpdate.NeedWriteToSap = objectToUpdateDTO.NeedWriteToSap;
                if (objectToUpdateDTO.NeedReadFromSap != objectToUpdate.NeedReadFromSap)
                    objectToUpdate.NeedReadFromSap = objectToUpdateDTO.NeedReadFromSap;
                if (objectToUpdateDTO.NeedReadFromMes != objectToUpdate.NeedReadFromMes)
                    objectToUpdate.NeedReadFromMes = objectToUpdateDTO.NeedReadFromMes;
                if (objectToUpdateDTO.NeedWriteToMes != objectToUpdate.NeedWriteToMes)
                    objectToUpdate.NeedWriteToMes = objectToUpdateDTO.NeedWriteToMes;
                if (objectToUpdateDTO.IsNdo != objectToUpdate.IsNdo)
                    objectToUpdate.IsNdo = objectToUpdateDTO.IsNdo;
                if (objectToUpdateDTO.IsArchive != objectToUpdate.IsArchive)
                    objectToUpdate.IsArchive = objectToUpdateDTO.IsArchive;


                _db.MesParam.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<MesParam, MesParamDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(int id, UpdateMode updateMode = UpdateMode.Update)
        {
            if (id > 0)
            {
                var objectToDelete = _db.MesParam.FirstOrDefault(u => u.Id == id);
                if (objectToDelete != null)
                {
                    if (updateMode == SD.UpdateMode.MoveToArchive)
                        objectToDelete.IsArchive = true;
                    if (updateMode == SD.UpdateMode.RestoreFromArchive)
                        objectToDelete.IsArchive = false;
                    _db.MesParam.Update(objectToDelete);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;

        }
    }
}
