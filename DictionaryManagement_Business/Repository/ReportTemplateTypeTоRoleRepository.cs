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
    public class ReportTemplateTypeTоRoleRepository : IReportTemplateTypeTоRoleRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ReportTemplateTypeTоRoleRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ReportTemplateTypeTоRoleDTO> Create(ReportTemplateTypeTоRoleDTO objectToAddDTO)
        {

            ReportTemplateTypeTоRole objectToAdd = new ReportTemplateTypeTоRole();
                
                objectToAdd.Id = objectToAddDTO.Id;
                objectToAdd.ReportTemplateTypeId = objectToAddDTO.ReportTemplateTypeId;
                objectToAdd.RoleId = objectToAddDTO.RoleId;
                objectToAdd.CanDownload = objectToAddDTO.CanDownload;
                objectToAdd.CanUpload = objectToAddDTO.CanUpload;
            
            var addedReportTemplateTypeTоRole = _db.ReportTemplateTypeTоRole.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<ReportTemplateTypeTоRole, ReportTemplateTypeTоRoleDTO>(addedReportTemplateTypeTоRole.Entity);
        }

        public async Task<ReportTemplateTypeTоRoleDTO> Get(int reportTemplateId, string roleId)
        {
            var objToGet = _db.ReportTemplateTypeTоRole.Include("ReportTemplateTypeFK").Include("RoleFK").
                            FirstOrDefaultAsync(u => u.ReportTemplateTypeId == reportTemplateId && u.RoleId.Trim().ToUpper() == roleId.Trim().ToUpper()).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<ReportTemplateTypeTоRole, ReportTemplateTypeTоRoleDTO>(objToGet);
            }
            return null;
        }

        public async Task<ReportTemplateTypeTоRoleDTO> GetById(int id)
        {
            var objToGet = _db.ReportTemplateTypeTоRole.Include("ReportTemplateTypeFK").Include("RoleFK").
                            FirstOrDefaultAsync(u => u.Id == id).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<ReportTemplateTypeTоRole, ReportTemplateTypeTоRoleDTO>(objToGet);
            }
            return null;
        }


        public async Task<IEnumerable<ReportTemplateTypeTоRoleDTO>> GetAll()
        {
            var hhh = _db.ReportTemplateTypeTоRole.Include("ReportTemplateTypeFK").Include("RoleFK");
            return _mapper.Map<IEnumerable<ReportTemplateTypeTоRole>, IEnumerable<ReportTemplateTypeTоRoleDTO>>(hhh);
            
        }

        public async Task<ReportTemplateTypeTоRoleDTO> Update(ReportTemplateTypeTоRoleDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.ReportTemplateTypeTоRole.Include("ReportTemplateTypeFK").Include("RoleFK").
                    FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {
                if (objectToUpdate.ReportTemplateTypeId != objectToUpdateDTO.ReportTemplateTypeDTOFK.Id)
                {
                    objectToUpdate.ReportTemplateTypeId = objectToUpdateDTO.ReportTemplateTypeDTOFK.Id;
                    objectToUpdate.ReportTemplateTypeFK = _mapper.Map<ReportTemplateTypeDTO, ReportTemplateType>(objectToUpdateDTO.ReportTemplateTypeDTOFK);
                }
                if (objectToUpdate.RoleId != objectToUpdateDTO.RoleDTOFK.Id)
                {
                    objectToUpdate.RoleId = objectToUpdateDTO.RoleDTOFK.Id;
                    objectToUpdate.RoleFK = _mapper.Map<RoleDTO, Role>(objectToUpdateDTO.RoleDTOFK);
                }

                if (objectToUpdate.CanDownload != objectToUpdateDTO.CanDownload)
                    _db.ReportTemplateTypeTоRole.Update(objectToUpdate);

                if (objectToUpdate.CanUpload != objectToUpdateDTO.CanUpload)
                    _db.ReportTemplateTypeTоRole.Update(objectToUpdate);

                await _db.SaveChangesAsync();
                return _mapper.Map<ReportTemplateTypeTоRole, ReportTemplateTypeTоRoleDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(int id)
        {
            if (id > 0)
            {
                var objectToDelete = _db.ReportTemplateTypeTоRole.FirstOrDefault(u => u.Id == id);
                if (objectToDelete != null)
                {
                    _db.ReportTemplateTypeTоRole.Remove(objectToDelete);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;

        }
    }
}
