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
    public interface IReportTemplateTypeTоRoleRepository
    {
        public Task<ReportTemplateTypeTоRoleDTO> Get(int reportTemplateTypeId, string roleId);
        public Task<ReportTemplateTypeTоRoleDTO> GetById(int id);
        public Task<IEnumerable<ReportTemplateTypeTоRoleDTO>> GetAll();
        public Task<ReportTemplateTypeTоRoleDTO> Update(ReportTemplateTypeTоRoleDTO objDTO);
        public Task<ReportTemplateTypeTоRoleDTO> Create(ReportTemplateTypeTоRoleDTO objectToAddDTO);
        public Task<int> Delete(int Id);
    }
}
