using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository.IRepository
{
    public interface IReportTemplateTоDepartmentRepository
    {
        public Task<ReportTemplateTоDepartmentDTO> Get(string reportTemplateId, int departmentId);
        public Task<ReportTemplateTоDepartmentDTO> GetById(int id);
        public Task<IEnumerable<ReportTemplateTоDepartmentDTO>> GetAll();
        public Task<ReportTemplateTоDepartmentDTO> Update(ReportTemplateTоDepartmentDTO objDTO);
        public Task<ReportTemplateTоDepartmentDTO> Create(ReportTemplateTоDepartmentDTO objectToAddDTO);
        public Task<int> Delete(int Id);
    }
}
