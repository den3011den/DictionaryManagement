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
    public class ReportTemplateTоDepartmentRepository : IReportTemplateTоDepartmentRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ReportTemplateTоDepartmentRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ReportTemplateTоDepartmentDTO> Create(ReportTemplateTоDepartmentDTO objectToAddDTO)
        {

            ReportTemplateTоDepartment objectToAdd = new ReportTemplateTоDepartment();
                
                objectToAdd.Id = objectToAddDTO.Id;
                objectToAdd.ReportTemplateId = objectToAddDTO.ReportTemplateId;
                objectToAdd.DepartmentId = objectToAddDTO.DepartmentId;


            var addedReportTemplateTоDepartment = _db.ReportTemplateTоDepartment.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<ReportTemplateTоDepartment, ReportTemplateTоDepartmentDTO>(addedReportTemplateTоDepartment.Entity);
        }

        public async Task<ReportTemplateTоDepartmentDTO> Get(string reportTemplateId, int departmentId)
        {
            var objToGet = _db.ReportTemplateTоDepartment.Include("ReportTemplateFK").Include("DepartmentFK").
                            FirstOrDefaultAsync(u => u.ReportTemplateId.Trim().ToUpper() == reportTemplateId.Trim().ToUpper() && u.DepartmentId == departmentId).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<ReportTemplateTоDepartment, ReportTemplateTоDepartmentDTO>(objToGet);
            }
            return null;
        }

        public async Task<ReportTemplateTоDepartmentDTO> GetById(int id)
        {
            var objToGet = _db.ReportTemplateTоDepartment.Include("ReportTemplateFK").Include("DepartmentFK").
                            FirstOrDefaultAsync(u => u.Id == id).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<ReportTemplateTоDepartment, ReportTemplateTоDepartmentDTO>(objToGet);
            }
            return null;
        }


        public async Task<IEnumerable<ReportTemplateTоDepartmentDTO>> GetAll()
        {
            var hhh = _db.ReportTemplateTоDepartment.Include("ReportTemplateFK").Include("DepartmentFK");
            return _mapper.Map<IEnumerable<ReportTemplateTоDepartment>, IEnumerable<ReportTemplateTоDepartmentDTO>>(hhh);
            
        }

        public async Task<ReportTemplateTоDepartmentDTO> Update(ReportTemplateTоDepartmentDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.ReportTemplateTоDepartment.Include("ReportTemplateFK").Include("DepartmentFK").
                    FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {

                if (objectToUpdate.ReportTemplateId != objectToUpdateDTO.ReportTemplateDTOFK.Id)
                {
                    objectToUpdate.ReportTemplateId = objectToUpdateDTO.ReportTemplateDTOFK.Id;                    
                    objectToUpdate.ReportTemplateFK = _mapper.Map<ReportTemplateDTO, ReportTemplate>(objectToUpdateDTO.ReportTemplateDTOFK);
                }
                if (objectToUpdate.DepartmentId != objectToUpdateDTO.DepartmentDTOFK.Id)
                {
                    objectToUpdate.DepartmentId = objectToUpdateDTO.DepartmentDTOFK.Id;
                    objectToUpdate.DepartmentFK = _mapper.Map<MesDepartmentDTO, MesDepartment>(objectToUpdateDTO.DepartmentDTOFK);
                }
                _db.ReportTemplateTоDepartment.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<ReportTemplateTоDepartment, ReportTemplateTоDepartmentDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(int id)
        {
            if (id > 0)
            {
                var objectToDelete = _db.ReportTemplateTоDepartment.FirstOrDefault(u => u.Id == id);
                if (objectToDelete != null)
                {
                    _db.ReportTemplateTоDepartment.Remove(objectToDelete);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;

        }
    }
}
