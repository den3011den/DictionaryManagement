﻿using DictionaryManagement_Common;
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
        public Task<string> GenerateExcelSapEquipmentDTO(string filename, IEnumerable<SapEquipmentDTO> data);
        public Task<string> GenerateExcelSapMaterialDTO(string filename, IEnumerable<SapMaterialDTO> data);
    }
}
