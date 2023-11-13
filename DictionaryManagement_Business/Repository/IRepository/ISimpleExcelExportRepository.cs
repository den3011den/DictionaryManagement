using DictionaryManagement_Common;
using DictionaryManagement_Models.IntDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository.IRepository
{
    public interface ISimpleExcelExportRepository
    {
        public Task<string> GenerateExcelReportEntity(string filename, IEnumerable<ReportEntityDTO> data);
        public Task<string> GenerateExcelMesParam(string filename, IEnumerable<MesParamDTO> data);
        public Task<string> GenerateExcelSapEquipment(string filename, IEnumerable<SapEquipmentDTO> data);
        public Task<string> GenerateExcelSapMaterial(string filename, IEnumerable<SapMaterialDTO> data);
        public Task<string> GenerateExcelUsers(string filename, IEnumerable<UserDTO> data);
        public Task<string> GenerateExcelADGroup(string filename, IEnumerable<ADGroupDTO> data);
        public Task<string> GenerateExcelRole(string filename, IEnumerable<RoleVMDTO> data);
        public Task<string> GenerateExcelMesDepartments(string filename, IEnumerable<MesDepartmentVMDTO> mesDepartmentVMDTOList, int maxLevel);

    }
}
