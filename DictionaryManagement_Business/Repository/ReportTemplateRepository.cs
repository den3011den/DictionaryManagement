using AutoMapper;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
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
    public class ReportTemplateRepository : IReportTemplateRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ReportTemplateRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ReportTemplateDTO> Create(ReportTemplateDTO objectToAddDTO)
        {


            ReportTemplate objectToAdd = new ReportTemplate();

            if (objectToAddDTO.Id != null && objectToAddDTO.Id != Guid.Empty)
                objectToAdd.Id = Guid.NewGuid();
            else
                objectToAdd.Id = objectToAddDTO.Id;

            if (objectToAddDTO.AddTime == null)
                objectToAdd.AddTime = DateTime.Now;
            else
                objectToAdd.AddTime = objectToAddDTO.AddTime;

            objectToAdd.AddUserId = objectToAddDTO.AddUserId;
            objectToAdd.Description = objectToAddDTO.Description;
            objectToAdd.ReportTemplateTypeId = objectToAddDTO.ReportTemplateTypeId;
            objectToAdd.DestDataTypeId = objectToAddDTO.DestDataTypeId;
            objectToAdd.DepartmentId = objectToAddDTO.DepartmentId;
            objectToAdd.TemplateFileName = objectToAddDTO.TemplateFileName;
            objectToAdd.IsArchive = objectToAddDTO.IsArchive;

            var addedReportTemplate = _db.ReportTemplate.Add(objectToAdd);
            _db.SaveChanges();
            return _mapper.Map<ReportTemplate, ReportTemplateDTO>(addedReportTemplate.Entity);
        }


        public async Task<ReportTemplateDTO> GetById(Guid id)
        {
            var objToGet = _db.ReportTemplate
                            .Include("AddUserFK")
                            .Include("ReportTemplateTypeFK")
                            .Include("DestDataTypeFK")
                            .Include("MesDepartmentFK")
                            .FirstOrDefault(u => u.Id == id);
            if (objToGet != null)
            {
                return _mapper.Map<ReportTemplate, ReportTemplateDTO>(objToGet);
            }
            return null;
        }


        public async Task<ReportTemplateDTO> GetByTemplateFileName(string templateFileName = "")
        {
            var objToGet = _db.ReportTemplate
                            .Include("AddUserFK")
                            .Include("ReportTemplateTypeFK")
                            .Include("DestDataTypeFK")
                            .Include("MesDepartmentFK")
                            .FirstOrDefault(u => u.TemplateFileName.Trim().ToUpper() == templateFileName.Trim().ToUpper());
            if (objToGet != null)
            {
                return _mapper.Map<ReportTemplate, ReportTemplateDTO>(objToGet);
            }
            return null;
        }

        public async Task<ReportTemplateDTO> GetByReportTemplateTypeIdAndDestDataTypeIdAndDepartmentId(int reportTemplateTypeId, int destDataTypeId, int departmentId)
        {

            if (reportTemplateTypeId > 0 && destDataTypeId > 0 && departmentId > 0)
            { 
                var objToGet = _db.ReportTemplate
                            .Include("AddUserFK")
                            .Include("ReportTemplateTypeFK")
                            .Include("DestDataTypeFK")
                            .Include("MesDepartmentFK")
                            .FirstOrDefault(u => u.ReportTemplateTypeId == reportTemplateTypeId && u.DestDataTypeId == destDataTypeId && u.DepartmentId == departmentId);
                if (objToGet != null)
                {
                    return _mapper.Map<ReportTemplate, ReportTemplateDTO>(objToGet);
                }
            }
            return null;
        }


        public async Task<IEnumerable<ReportTemplateDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All)
        {
            if (selectDictionaryScope == SD.SelectDictionaryScope.ArchiveOnly)
            {
                var hhh2 = _db.ReportTemplate
                            .Include("AddUserFK")
                            .Include("ReportTemplateTypeFK")
                            .Include("DestDataTypeFK")
                            .Include("MesDepartmentFK")
                            .Where(u => u.IsArchive == true);
                return _mapper.Map<IEnumerable<ReportTemplate>, IEnumerable<ReportTemplateDTO>>(hhh2);
            }
            if (selectDictionaryScope == SD.SelectDictionaryScope.NotArchiveOnly)
            {
                var hhh3 = _db.ReportTemplate
                            .Include("AddUserFK")
                            .Include("ReportTemplateTypeFK")
                            .Include("DestDataTypeFK")
                            .Include("MesDepartmentFK")
                            .Where(u => u.IsArchive != true);
                return _mapper.Map<IEnumerable<ReportTemplate>, IEnumerable<ReportTemplateDTO>>(hhh3);

            }
            var hhh1 = _db.ReportTemplate
                            .Include("AddUserFK")
                            .Include("ReportTemplateTypeFK")
                            .Include("DestDataTypeFK")
                            .Include("MesDepartmentFK");
            return _mapper.Map<IEnumerable<ReportTemplate>, IEnumerable<ReportTemplateDTO>>(hhh1);
        }


        public async Task<ReportTemplateDTO> Update(ReportTemplateDTO objectToUpdateDTO)
        {
            bool wasChanged = false;

            var objectToUpdate = _db.ReportTemplate
                            .Include("AddUserFK")
                            .Include("ReportTemplateTypeFK")
                            .Include("DestDataTypeFK")
                            .Include("MesDepartmentFK")
                        .FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);

            if (objectToUpdate != null)
            {
                if (objectToUpdateDTO.AddUserId == null || objectToUpdateDTO.AddUserId == Guid.Empty)
                {
                    objectToUpdate.AddUserId = Guid.Empty;
                    objectToUpdate.AddUserFK = null;
                }
                else
                {
                    if (objectToUpdate.AddUserId != objectToUpdateDTO.AddUserId)
                    {
                        objectToUpdate.AddUserId = objectToUpdateDTO.AddUserId;
                        var objectUserToUpdate = _db.User.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.AddUserId);
                        objectToUpdate.AddUserFK = objectUserToUpdate;
                    }
                }

                if (objectToUpdateDTO.ReportTemplateTypeId <= 0)
                {
                    objectToUpdate.ReportTemplateTypeId = 0;
                    objectToUpdate.ReportTemplateTypeFK = null;
                }
                else
                {
                    if (objectToUpdate.ReportTemplateTypeId != objectToUpdateDTO.ReportTemplateTypeId)
                    {                        
                        objectToUpdate.ReportTemplateTypeId = objectToUpdateDTO.ReportTemplateTypeId;
                        var objectReportTemplateTypeToUpdate = _db.ReportTemplateType.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.ReportTemplateTypeId);
                        objectToUpdate.ReportTemplateTypeFK = objectReportTemplateTypeToUpdate;
                    }
                }

                if (objectToUpdateDTO.DestDataTypeId <= 0)
                {
                    objectToUpdate.DestDataTypeId = 0;
                    objectToUpdate.DestDataTypeFK = null;
                }
                else
                {
                    if (objectToUpdate.DestDataTypeId != objectToUpdateDTO.DestDataTypeId)
                    {
                        objectToUpdate.DestDataTypeId = objectToUpdateDTO.DestDataTypeId;
                        var objectDataTypeToUpdate = _db.DataType.
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.DestDataTypeId);
                        objectToUpdate.DestDataTypeFK = objectDataTypeToUpdate;
                    }
                }

                if (objectToUpdateDTO.DepartmentId <= 0)
                {
                    objectToUpdate.DepartmentId = 0;
                    objectToUpdate.MesDepartmentFK = null;
                }
                else
                {
                    if (objectToUpdate.DepartmentId != objectToUpdateDTO.DepartmentId)
                    {
                        objectToUpdate.DepartmentId = objectToUpdateDTO.DepartmentId;
                        var objectMesDepartmentToUpdate = _db.MesDepartment.FirstOrDefault(u => u.Id == objectToUpdateDTO.DepartmentId);
                        objectToUpdate.MesDepartmentFK = objectMesDepartmentToUpdate;
                    }
                }


                if (objectToUpdateDTO.AddTime != objectToUpdate.AddTime)
                    objectToUpdate.AddTime = objectToUpdateDTO.AddTime;
                if (objectToUpdateDTO.Description != objectToUpdate.Description)
                    objectToUpdate.Description = objectToUpdateDTO.Description;
                if (objectToUpdateDTO.TemplateFileName != objectToUpdate.TemplateFileName)
                    objectToUpdate.TemplateFileName = objectToUpdateDTO.TemplateFileName;
                if (objectToUpdateDTO.IsArchive != objectToUpdate.IsArchive)
                    objectToUpdate.IsArchive = objectToUpdateDTO.IsArchive;


                _db.ReportTemplate.Update(objectToUpdate);
                _db.SaveChanges();
                return _mapper.Map<ReportTemplate, ReportTemplateDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(Guid id, UpdateMode updateMode = UpdateMode.Update)
        {
            if (id != null && id != Guid.Empty)
            {
                var objectToDelete = _db.ReportTemplate.FirstOrDefault(u => u.Id == id);
                if (objectToDelete != null)
                {
                    if (updateMode == SD.UpdateMode.MoveToArchive)
                        objectToDelete.IsArchive = true;
                    if (updateMode == SD.UpdateMode.RestoreFromArchive)
                        objectToDelete.IsArchive = false;
                    _db.ReportTemplate.Update(objectToDelete);
                    return _db.SaveChanges();
                }
            }
            return 0;

        }
    }
}