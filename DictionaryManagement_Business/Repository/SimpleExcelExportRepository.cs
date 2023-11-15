using AutoMapper;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using DND.EFCoreWithNoLock.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;
using ClosedXML.Excel;
using System.Reflection.Metadata.Ecma335;
using System.IO;

namespace DictionaryManagement_Business.Repository
{
    public class SimpleExcelExportRepository : ISimpleExcelExportRepository
    {
        private readonly ISettingsRepository _settingsRepository;

        public SimpleExcelExportRepository(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<string> GenerateExcelReportEntity(string filename, IEnumerable<ReportEntityDTO> data)
        {

            string pathVar = (await _settingsRepository.GetByName("TempFilePath")).Value;
            string fullfilepath = System.IO.Path.Combine(pathVar, filename);

            using var wbook = new XLWorkbook();
            {

                var ws = wbook.AddWorksheet("ReportEntity");

                int excelRowNum = 1;
                int excelColNum = 1;


                ws.Cell(excelRowNum, excelColNum).Value = "ИД экземпляра отчёта";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "ИД шаблона отчёта";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Тип шаблона отчёта";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Начало интервала отчёта";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Окончание интервала отчёта";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "ИД производства у экземпляра отчёта";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование производства у экземпляра отчёта";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "ИД производства у шаблона отчёта";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование производства у шаблона отчёта";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время скачивания (Download)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Кто скачал (Download)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время загрузки (Upload)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Кто загрузил (Upload)";

                ws.Row(excelRowNum).Style.Font.SetBold(true);
                ws.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;

                excelRowNum = 2;
                foreach (ReportEntityDTO reportEntity in data)
                {
                    excelColNum = 1;

                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.Id.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.ReportTemplateId.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.ReportTemplateDTOFK.ReportTemplateTypeDTOFK.Name.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.ReportTimeStart.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.ReportTimeEnd.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.ReportDepartmentId.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.ReportDepartmentDTOFK == null ? "" : reportEntity.ReportDepartmentDTOFK.ShortName.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.ReportTemplateDTOFK.MesDepartmentDTOFK.Id.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.ReportTemplateDTOFK.MesDepartmentDTOFK.ShortName.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.DownloadTime.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.DownloadUserDTOFK == null ? "" : reportEntity.DownloadUserDTOFK.UserName.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.UploadTime.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = reportEntity.UploadUserDTOFK == null ? "" : reportEntity.UploadUserDTOFK.UserName.ToString();

                    excelRowNum++;
                }

                for (var j = 1; j <= excelColNum; j++)
                    ws.Column(j).AdjustToContents();
                wbook.SaveAs(fullfilepath);
                if (wbook != null)
                    wbook.Dispose();
            }
            return fullfilepath;
        }


        public async Task<string> GenerateExcelMesParam(string filename, IEnumerable<MesParamDTO> data)
        {

            string pathVar = (await _settingsRepository.GetByName("TempFilePath")).Value;
            string fullfilepath = System.IO.Path.Combine(pathVar, filename);

            using var wbook = new XLWorkbook();
            {

                var ws = wbook.AddWorksheet("MesParam");

                int excelRowNum = 1;
                int excelColNum = 1;


                ws.Cell(excelRowNum, excelColNum).Value = "ИД тэга СИР (Id)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Код тэга (Code)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование тэга (Name)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Точка измерения (TI)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование точки измерения (NameTI)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Технологическое место (TM)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование технологического места (NameTM)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Описание (Description)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Источник (MesParamSourceType.Name)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Тэг источника (MesParamSourceLink)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Код производства (DepartmentId)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование производства (MesDepartment.ShortName)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "ИД источника Sap (SapEquipmentIdSource)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование источника Sap (SapEquipment.ErpPlantId + ErpId + Name)";

                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "ИД приёмника Sap (SapEquipmentIdDest)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование приёмника Sap (SapEquipment.ErpPlantId + ErpId + Name)";

                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "ИД материала Sap (SapMaterialId)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Код АСВ НСИ материала Sap (SapMaterial.Code)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование материала Sap (SapMaterial.Name)";

                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "ИД ед.изм. Sap (SapUnitOfMeasureId)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование ед.изм. Sap (SapUnitOfMeasure.ShortName)";

                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время запроса данных в прошлое в днях (DaysRequestInPast)";

                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Читать из SAP (NeedReadFromSap)";

                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Передавать в SAP (NeedWriteToSap)";

                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Читать из MES (NeedReadFromMes)";

                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Передавать в MES (NeedWriteToMes)";

                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Является тэгом НДО (IsNdo)";

                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Коэф. пересчёта данных по тэгу из ед.изм. MES в ед.изм СИР (MesToSirUnitOfMeasureKoef)";

                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "В архиве (IsArchive)";

                ws.Row(excelRowNum).Style.Font.SetBold(true);
                ws.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;

                excelRowNum = 2;
                foreach (MesParamDTO mesParamDTO in data)
                {
                    excelColNum = 1;

                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.Id.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.Code;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.Name == null ? "" : mesParamDTO.Name;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.TI == null ? "" : mesParamDTO.TI;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.NameTI == null ? "" : mesParamDTO.NameTI;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.TM == null ? "" : mesParamDTO.TM;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.NameTM == null ? "" : mesParamDTO.NameTM;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.Description == null ? "" : mesParamDTO.Description;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.MesParamSourceTypeDTOFK == null ? "" : mesParamDTO.MesParamSourceTypeDTOFK.Name;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.MesParamSourceLink == null ? "" : mesParamDTO.MesParamSourceLink;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.DepartmentId == null ? "" : mesParamDTO.DepartmentId.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.MesDepartmentDTOFK == null ? "" : mesParamDTO.MesDepartmentDTOFK.ShortName;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.SapEquipmentIdSource == null ? "" : mesParamDTO.SapEquipmentIdSource.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.SapEquipmentSourceDTOFK == null ? "" : mesParamDTO.SapEquipmentSourceDTOFK.ErpPlantId + "|" + mesParamDTO.SapEquipmentSourceDTOFK.ErpId + " " + mesParamDTO.SapEquipmentSourceDTOFK.Name;

                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.SapEquipmentIdDest == null ? "" : mesParamDTO.SapEquipmentIdDest.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.SapEquipmentDestDTOFK == null ? "" : mesParamDTO.SapEquipmentDestDTOFK.ErpPlantId + "|" + mesParamDTO.SapEquipmentDestDTOFK.ErpId + " " + mesParamDTO.SapEquipmentDestDTOFK.Name;

                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.SapMaterialId == null ? "" : mesParamDTO.SapMaterialId.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.SapMaterialDTOFK == null ? "" : mesParamDTO.SapMaterialDTOFK.Code;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.SapMaterialDTOFK == null ? "" : mesParamDTO.SapMaterialDTOFK.Name;

                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.SapUnitOfMeasureId == null ? "" : mesParamDTO.SapUnitOfMeasureId.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.SapUnitOfMeasureDTOFK == null ? "" : mesParamDTO.SapUnitOfMeasureDTOFK.ShortName;

                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.DaysRequestInPast == null ? "" : mesParamDTO.DaysRequestInPast.ToString();

                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.NeedReadFromSap == true ? "Да" : "";

                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.NeedWriteToSap == true ? "Да" : "";

                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.NeedReadFromMes == true ? "Да" : "";

                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.NeedWriteToMes == true ? "Да" : "";

                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.IsNdo == true ? "Да" : "";

                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.MesToSirUnitOfMeasureKoef == null ? "" : mesParamDTO.MesToSirUnitOfMeasureKoef.ToString();

                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesParamDTO.IsArchive == true ? "Да" : "";

                    excelRowNum++;
                }

                for (var j = 1; j <= excelColNum; j++)
                    ws.Column(j).AdjustToContents();
                wbook.SaveAs(fullfilepath);
                if (wbook != null)
                    wbook.Dispose();
            }
            return fullfilepath;
        }

        public async Task<string> GenerateExcelSapEquipment(string filename, IEnumerable<SapEquipmentDTO> data)
        {

            string pathVar = (await _settingsRepository.GetByName("TempFilePath")).Value;
            string fullfilepath = System.IO.Path.Combine(pathVar, filename);

            using var wbook = new XLWorkbook();
            {

                var ws = wbook.AddWorksheet("SapEquipment");

                int excelRowNum = 1;
                int excelColNum = 1;

                ws.Cell(excelRowNum, excelColNum).Value = "ИД (Id)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Код завода SAP (ErpPlantId)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Код ресурса/склада SAP (ErpId)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование (Name)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Является складом (IsWarehouse)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "В архиве (IsArchive)";

                ws.Row(excelRowNum).Style.Font.SetBold(true);
                ws.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;

                excelRowNum = 2;
                foreach (SapEquipmentDTO sapEquipmentDTO in data)
                {
                    excelColNum = 1;

                    ws.Cell(excelRowNum, excelColNum).Value = sapEquipmentDTO.Id.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapEquipmentDTO.ErpPlantId;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapEquipmentDTO.ErpId;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapEquipmentDTO.Name;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapEquipmentDTO.IsWarehouse == true ? "Да" : "";
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapEquipmentDTO.IsArchive == true ? "Да" : "";

                    excelRowNum++;
                }

                for (var j = 1; j <= excelColNum; j++)
                    ws.Column(j).AdjustToContents();
                wbook.SaveAs(fullfilepath);
                if (wbook != null)
                    wbook.Dispose();
            }
            return fullfilepath;
        }

        public async Task<string> GenerateExcelSapMaterial(string filename, IEnumerable<SapMaterialDTO> data)
        {

            string pathVar = (await _settingsRepository.GetByName("TempFilePath")).Value;
            string fullfilepath = System.IO.Path.Combine(pathVar, filename);

            using var wbook = new XLWorkbook();
            {

                var ws = wbook.AddWorksheet("SapMaterial");

                int excelRowNum = 1;
                int excelColNum = 1;

                ws.Cell(excelRowNum, excelColNum).Value = "ИД (Id)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Код АСВ НСИ (Code)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование (Name)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Сокр. наименование (ShortName)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "В архиве (IsArchive)";

                ws.Row(excelRowNum).Style.Font.SetBold(true);
                ws.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;

                excelRowNum = 2;
                foreach (SapMaterialDTO sapMaterialDTO in data)
                {
                    excelColNum = 1;

                    ws.Cell(excelRowNum, excelColNum).Value = sapMaterialDTO.Id.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapMaterialDTO.Code;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapMaterialDTO.Name;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapMaterialDTO.ShortName;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapMaterialDTO.IsArchive == true ? "Да" : "";

                    excelRowNum++;
                }

                for (var j = 1; j <= excelColNum; j++)
                    ws.Column(j).AdjustToContents();
                wbook.SaveAs(fullfilepath);
                if (wbook != null)
                    wbook.Dispose();
            }
            return fullfilepath;
        }


        public async Task<string> GenerateExcelUsers(string filename, IEnumerable<UserDTO> data)
        {

            string pathVar = (await _settingsRepository.GetByName("TempFilePath")).Value;
            string fullfilepath = System.IO.Path.Combine(pathVar, filename);

            using var wbook = new XLWorkbook();
            {

                var ws = wbook.AddWorksheet("Users");

                int excelRowNum = 1;
                int excelColNum = 1;

                ws.Cell(excelRowNum, excelColNum).Value = "ИД (Id)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Логин (Login)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование (UserName)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Описание (Description)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Синхронизируется с AD (IsSyncWithAD)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "В архиве (IsArchive)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время последней синхронизации с AD (SyncWithADGroupsLastTime)";

                ws.Row(excelRowNum).Style.Font.SetBold(true);
                ws.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;

                excelRowNum = 2;
                foreach (UserDTO userDTO in data)
                {
                    excelColNum = 1;

                    ws.Cell(excelRowNum, excelColNum).Value = userDTO.Id.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = userDTO.Login;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = userDTO.UserName;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = userDTO.Description == null ? "" : userDTO.Description;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = userDTO.IsSyncWithAD == true ? "Да" : "";
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = userDTO.IsArchive == true ? "Да" : "";
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = userDTO.SyncWithADGroupsLastTime.ToString();

                    excelRowNum++;
                }

                for (var j = 1; j <= excelColNum; j++)
                    ws.Column(j).AdjustToContents();
                wbook.SaveAs(fullfilepath);
                if (wbook != null)
                    wbook.Dispose();
            }
            return fullfilepath;
        }


        public async Task<string> GenerateExcelADGroup(string filename, IEnumerable<ADGroupDTO> data)
        {

            string pathVar = (await _settingsRepository.GetByName("TempFilePath")).Value;
            string fullfilepath = System.IO.Path.Combine(pathVar, filename);

            using var wbook = new XLWorkbook();
            {

                var ws = wbook.AddWorksheet("ADGroups");

                int excelRowNum = 1;
                int excelColNum = 1;

                ws.Cell(excelRowNum, excelColNum).Value = "ИД (Id)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование (Name)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Описание (Description)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "В архиве (IsArchive)";

                ws.Row(excelRowNum).Style.Font.SetBold(true);
                ws.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;

                excelRowNum = 2;
                foreach (ADGroupDTO adGroupDTO in data)
                {
                    excelColNum = 1;

                    ws.Cell(excelRowNum, excelColNum).Value = adGroupDTO.Id.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = adGroupDTO.Name;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = adGroupDTO.Description == null ? "" : adGroupDTO.Description;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = adGroupDTO.IsArchive == true ? "Да" : "";

                    excelRowNum++;
                }

                for (var j = 1; j <= excelColNum; j++)
                    ws.Column(j).AdjustToContents();
                wbook.SaveAs(fullfilepath);
                if (wbook != null)
                    wbook.Dispose();
            }
            return fullfilepath;
        }

        public async Task<string> GenerateExcelRole(string filename, IEnumerable<RoleVMDTO> data)
        {

            string pathVar = (await _settingsRepository.GetByName("TempFilePath")).Value;
            string fullfilepath = System.IO.Path.Combine(pathVar, filename);

            using var wbook = new XLWorkbook();
            {

                var wsRole = wbook.AddWorksheet("Roles");
                var wsUserToRole = wbook.AddWorksheet("UserToRole");
                var wsReportTemplateTypeTоRole = wbook.AddWorksheet("ReportTemplateTypeTоRole");
                var wsRoleToADGroup = wbook.AddWorksheet("RoleToADGroup");
                var wsRoleToDepartment = wbook.AddWorksheet("RoleToDepartment");

                int excelRowNum = 1;
                int excelColNum = 1;

                wsRole.Cell(excelRowNum, excelColNum).Value = "ИД (Id)";
                excelColNum++;
                wsRole.Cell(excelRowNum, excelColNum).Value = "Наименование (Name)";
                excelColNum++;
                wsRole.Cell(excelRowNum, excelColNum).Value = "Описание (Description)";
                excelColNum++;
                wsRole.Cell(excelRowNum, excelColNum).Value = "В архиве (IsArchive)";

                wsRole.Row(excelRowNum).Style.Font.SetBold(true);
                wsRole.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;

                excelRowNum = 1;
                excelColNum = 1;

                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "ИД связки UserToRole (UserToRole.Id)";
                excelColNum++;
                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "ИД роли СИР (Role.Id)";
                excelColNum++;
                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "Наименование роли СИР (Role.Name)";
                excelColNum++;
                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "Описание роли СИР (Role.Description)";
                excelColNum++;
                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "В архиве (Role.IsArchive)";

                excelColNum++;
                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "ИД пользователя (User.Id)";
                excelColNum++;
                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "Логин пользователя (User.Login)";
                excelColNum++;
                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "Наименование пользователя (User.UserName)";
                excelColNum++;
                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "Описание пользователя (User.Description)";
                excelColNum++;
                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "Синхронизируется с AD (User.IsSyncWithAD)";
                excelColNum++;
                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "Время последней синхронизации с AD (User.SyncWithADGroupsLastTime)";
                excelColNum++;
                wsUserToRole.Cell(excelRowNum, excelColNum).Value = "В архиве (User.IsArchive)";

                wsUserToRole.Row(excelRowNum).Style.Font.SetBold(true);
                wsUserToRole.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;


                excelRowNum = 1;
                excelColNum = 1;

                wsReportTemplateTypeTоRole.Cell(excelRowNum, excelColNum).Value = "ИД связки ReportTemplateTypeTоRole (ReportTemplateTypeTоRole.Id)";
                excelColNum++;
                wsReportTemplateTypeTоRole.Cell(excelRowNum, excelColNum).Value = "ИД роли СИР (Role.Id)";
                excelColNum++;
                wsReportTemplateTypeTоRole.Cell(excelRowNum, excelColNum).Value = "Наименование роли СИР (Role.Name)";
                excelColNum++;
                wsReportTemplateTypeTоRole.Cell(excelRowNum, excelColNum).Value = "Описание роли СИР (Role.Description)";
                excelColNum++;
                wsReportTemplateTypeTоRole.Cell(excelRowNum, excelColNum).Value = "В архиве (Role.IsArchive)";

                excelColNum++;
                wsReportTemplateTypeTоRole.Cell(excelRowNum, excelColNum).Value = "ИД типа шаблона отчёта (ReportTemplateType.Id)";
                excelColNum++;
                wsReportTemplateTypeTоRole.Cell(excelRowNum, excelColNum).Value = "Наименование типа шаблона отчёта (ReportTemplateType.Name)";
                excelColNum++;
                wsReportTemplateTypeTоRole.Cell(excelRowNum, excelColNum).Value = "Расчёт автоматически (ReportTemplateType.NeedAutoCalc)";
                excelColNum++;
                wsReportTemplateTypeTоRole.Cell(excelRowNum, excelColNum).Value = "Право на чтение (ReportTemplateTypeTоRole.CanDownload)";
                excelColNum++;
                wsReportTemplateTypeTоRole.Cell(excelRowNum, excelColNum).Value = "Право на запись (ReportTemplateTypeTоRole.CanUpload)";
                excelColNum++;
                wsReportTemplateTypeTоRole.Cell(excelRowNum, excelColNum).Value = "В архиве (ReportTemplateType.IsArchive)";

                wsReportTemplateTypeTоRole.Row(excelRowNum).Style.Font.SetBold(true);
                wsReportTemplateTypeTоRole.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;

                excelRowNum = 1;
                excelColNum = 1;

                wsRoleToADGroup.Cell(excelRowNum, excelColNum).Value = "ИД связки RoleToADGroup (RoleToADGroup.Id)";
                excelColNum++;
                wsRoleToADGroup.Cell(excelRowNum, excelColNum).Value = "ИД роли СИР (Role.Id)";
                excelColNum++;
                wsRoleToADGroup.Cell(excelRowNum, excelColNum).Value = "Наименование роли СИР (Role.Name)";
                excelColNum++;
                wsRoleToADGroup.Cell(excelRowNum, excelColNum).Value = "Описание роли СИР (Role.Description)";
                excelColNum++;
                wsRoleToADGroup.Cell(excelRowNum, excelColNum).Value = "В архиве (Role.IsArchive)";

                excelColNum++;
                wsRoleToADGroup.Cell(excelRowNum, excelColNum).Value = "ИД группы AD (ADGroup.Id)";
                excelColNum++;
                wsRoleToADGroup.Cell(excelRowNum, excelColNum).Value = "Наименование AD группы (ADGroup.Name)";
                excelColNum++;
                wsRoleToADGroup.Cell(excelRowNum, excelColNum).Value = "Описание AD группы (ADGroup.Description)";
                excelColNum++;
                wsRoleToADGroup.Cell(excelRowNum, excelColNum).Value = "В архиве (ADGroup.IsArchive)";

                wsRoleToADGroup.Row(excelRowNum).Style.Font.SetBold(true);
                wsRoleToADGroup.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;


                excelRowNum = 1;
                excelColNum = 1;

                wsRoleToDepartment.Cell(excelRowNum, excelColNum).Value = "ИД связки RoleToDepartment (RoleToDepartment.Id)";
                excelColNum++;
                wsRoleToDepartment.Cell(excelRowNum, excelColNum).Value = "ИД роли СИР (Role.Id)";
                excelColNum++;
                wsRoleToDepartment.Cell(excelRowNum, excelColNum).Value = "Наименование роли СИР (Role.Name)";
                excelColNum++;
                wsRoleToDepartment.Cell(excelRowNum, excelColNum).Value = "Описание роли СИР (Role.Description)";
                excelColNum++;
                wsRoleToDepartment.Cell(excelRowNum, excelColNum).Value = "В архиве (Role.IsArchive)";

                excelColNum++;
                wsRoleToDepartment.Cell(excelRowNum, excelColNum).Value = "ИД производства (MesDepartment.Id)";
                excelColNum++;
                wsRoleToDepartment.Cell(excelRowNum, excelColNum).Value = "Код производства (MesDepartment.MesCode)";
                excelColNum++;
                wsRoleToDepartment.Cell(excelRowNum, excelColNum).Value = "Наименование производства (MesDepartment.Name)";
                excelColNum++;
                wsRoleToDepartment.Cell(excelRowNum, excelColNum).Value = "Сокр. наименование производства (MesDepartment.ShortName)";
                excelColNum++;
                wsRoleToDepartment.Cell(excelRowNum, excelColNum).Value = "В архиве (MesDepartment.IsArchive)";

                wsRoleToDepartment.Row(excelRowNum).Style.Font.SetBold(true);
                wsRoleToDepartment.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;

                excelRowNum = 2;

                int wsRoleRowNum = 2;
                int wsRoleColNum = 1;

                int wsUserToRoleRowNum = 2;
                int wsUserToRoleColNum = 1;

                int wsReportTemplateTypeTоRoleRowNum = 2;
                int wsReportTemplateTypeTоRoleColNum = 1;

                int wsRoleToADGroupRowNum = 2;
                int wsRoleToADGroupColNum = 1;

                int wsRoleToDepartmentRowNum = 2;
                int wsRoleToDepartmentColNum = 1;

                foreach (RoleVMDTO roleVMDTO in data)
                {
                    wsRoleColNum = 1;

                    wsRole.Cell(wsRoleRowNum, wsRoleColNum).Value = roleVMDTO.Id.ToString();
                    wsRoleColNum++;
                    wsRole.Cell(wsRoleRowNum, wsRoleColNum).Value = roleVMDTO.Name;
                    wsRoleColNum++;
                    wsRole.Cell(wsRoleRowNum, wsRoleColNum).Value = roleVMDTO.Description == null ? "" : roleVMDTO.Description;
                    wsRoleColNum++;
                    wsRole.Cell(wsRoleRowNum, wsRoleColNum).Value = roleVMDTO.IsArchive == true ? "Да" : "";

                    if (roleVMDTO.UserToRoleDTOs != null)
                    {
                        foreach (UserToRoleDTO userToRoleDTO in roleVMDTO.UserToRoleDTOs)
                        {
                            wsUserToRoleColNum = 1;

                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.Id.ToString();
                            wsUserToRoleColNum++;
                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.RoleDTOFK.Id.ToString();
                            wsUserToRoleColNum++;
                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.RoleDTOFK.Name;
                            wsUserToRoleColNum++;
                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.RoleDTOFK.Description == null ? "" : userToRoleDTO.RoleDTOFK.Description;
                            wsUserToRoleColNum++;
                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.RoleDTOFK.IsArchive == true ? "Да" : "";

                            wsUserToRoleColNum++;
                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.UserDTOFK.Id.ToString();
                            wsUserToRoleColNum++;
                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.UserDTOFK.Login;
                            wsUserToRoleColNum++;
                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.UserDTOFK.UserName;
                            wsUserToRoleColNum++;
                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.UserDTOFK.Description == null ? "" : userToRoleDTO.UserDTOFK.Description;
                            wsUserToRoleColNum++;
                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.UserDTOFK.IsSyncWithAD == true ? "Да" : "";
                            wsUserToRoleColNum++;
                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.UserDTOFK.SyncWithADGroupsLastTime.ToString();
                            wsUserToRoleColNum++;
                            wsUserToRole.Cell(wsUserToRoleRowNum, wsUserToRoleColNum).Value = userToRoleDTO.UserDTOFK.IsArchive == true ? "Да" : "";

                            wsUserToRoleRowNum++;
                        }
                    }

                    if (roleVMDTO.ReportTemplateTypeTоRoleDTOs != null)
                    {
                        foreach (ReportTemplateTypeTоRoleDTO reportTemplateTypeTоRoleDTO in roleVMDTO.ReportTemplateTypeTоRoleDTOs)
                        {
                            wsReportTemplateTypeTоRoleColNum = 1;

                            wsReportTemplateTypeTоRole.Cell(wsReportTemplateTypeTоRoleRowNum, wsReportTemplateTypeTоRoleColNum).Value = reportTemplateTypeTоRoleDTO.Id;
                            wsReportTemplateTypeTоRoleColNum++;
                            wsReportTemplateTypeTоRole.Cell(wsReportTemplateTypeTоRoleRowNum, wsReportTemplateTypeTоRoleColNum).Value = reportTemplateTypeTоRoleDTO.RoleDTOFK.Id.ToString();
                            wsReportTemplateTypeTоRoleColNum++;
                            wsReportTemplateTypeTоRole.Cell(wsReportTemplateTypeTоRoleRowNum, wsReportTemplateTypeTоRoleColNum).Value = reportTemplateTypeTоRoleDTO.RoleDTOFK.Name;
                            wsReportTemplateTypeTоRoleColNum++;
                            wsReportTemplateTypeTоRole.Cell(wsReportTemplateTypeTоRoleRowNum, wsReportTemplateTypeTоRoleColNum).Value = reportTemplateTypeTоRoleDTO.RoleDTOFK.Description == null ? "" : reportTemplateTypeTоRoleDTO.RoleDTOFK.Description;
                            wsReportTemplateTypeTоRoleColNum++;
                            wsReportTemplateTypeTоRole.Cell(wsReportTemplateTypeTоRoleRowNum, wsReportTemplateTypeTоRoleColNum).Value = reportTemplateTypeTоRoleDTO.RoleDTOFK.IsArchive == true ? "Да" : "";

                            wsReportTemplateTypeTоRoleColNum++;
                            wsReportTemplateTypeTоRole.Cell(wsReportTemplateTypeTоRoleRowNum, wsReportTemplateTypeTоRoleColNum).Value = reportTemplateTypeTоRoleDTO.ReportTemplateTypeDTOFK.Id.ToString();
                            wsReportTemplateTypeTоRoleColNum++;
                            wsReportTemplateTypeTоRole.Cell(wsReportTemplateTypeTоRoleRowNum, wsReportTemplateTypeTоRoleColNum).Value = reportTemplateTypeTоRoleDTO.ReportTemplateTypeDTOFK.Name;
                            wsReportTemplateTypeTоRoleColNum++;
                            wsReportTemplateTypeTоRole.Cell(wsReportTemplateTypeTоRoleRowNum, wsReportTemplateTypeTоRoleColNum).Value = reportTemplateTypeTоRoleDTO.ReportTemplateTypeDTOFK.NeedAutoCalc == true ? "Да" : "";
                            wsReportTemplateTypeTоRoleColNum++;
                            wsReportTemplateTypeTоRole.Cell(wsReportTemplateTypeTоRoleRowNum, wsReportTemplateTypeTоRoleColNum).Value = reportTemplateTypeTоRoleDTO.CanDownload == true ? "Да" : "";
                            wsReportTemplateTypeTоRoleColNum++;
                            wsReportTemplateTypeTоRole.Cell(wsReportTemplateTypeTоRoleRowNum, wsReportTemplateTypeTоRoleColNum).Value = reportTemplateTypeTоRoleDTO.CanUpload == true ? "Да" : "";
                            wsReportTemplateTypeTоRoleColNum++;
                            wsReportTemplateTypeTоRole.Cell(wsReportTemplateTypeTоRoleRowNum, wsReportTemplateTypeTоRoleColNum).Value = reportTemplateTypeTоRoleDTO.ReportTemplateTypeDTOFK.IsArchive == true ? "Да" : "";

                            wsReportTemplateTypeTоRoleRowNum++;
                        }
                    }

                    if (roleVMDTO.RoleToADGroupDTOs != null)
                    {
                        foreach (RoleToADGroupDTO roleToADGroupDTO in roleVMDTO.RoleToADGroupDTOs)
                        {
                            wsRoleToADGroupColNum = 1;

                            wsRoleToADGroup.Cell(wsRoleToADGroupRowNum, wsRoleToADGroupColNum).Value = roleToADGroupDTO.Id.ToString();
                            wsRoleToADGroupColNum++;
                            wsRoleToADGroup.Cell(wsRoleToADGroupRowNum, wsRoleToADGroupColNum).Value = roleToADGroupDTO.RoleDTOFK.Id.ToString();
                            wsRoleToADGroupColNum++;
                            wsRoleToADGroup.Cell(wsRoleToADGroupRowNum, wsRoleToADGroupColNum).Value = roleToADGroupDTO.RoleDTOFK.Name;
                            wsRoleToADGroupColNum++;
                            wsRoleToADGroup.Cell(wsRoleToADGroupRowNum, wsRoleToADGroupColNum).Value = roleToADGroupDTO.RoleDTOFK.Description == null ? "" : roleToADGroupDTO.RoleDTOFK.Description;
                            wsRoleToADGroupColNum++;
                            wsRoleToADGroup.Cell(wsRoleToADGroupRowNum, wsRoleToADGroupColNum).Value = roleToADGroupDTO.RoleDTOFK.IsArchive == true ? "Да" : "";

                            wsRoleToADGroupColNum++;
                            wsRoleToADGroup.Cell(wsRoleToADGroupRowNum, wsRoleToADGroupColNum).Value = roleToADGroupDTO.ADGroupDTOFK.Id.ToString();
                            wsRoleToADGroupColNum++;
                            wsRoleToADGroup.Cell(wsRoleToADGroupRowNum, wsRoleToADGroupColNum).Value = roleToADGroupDTO.ADGroupDTOFK.Name;
                            wsRoleToADGroupColNum++;
                            wsRoleToADGroup.Cell(wsRoleToADGroupRowNum, wsRoleToADGroupColNum).Value = roleToADGroupDTO.ADGroupDTOFK.Description == null ? "" : roleToADGroupDTO.ADGroupDTOFK.Description;
                            wsRoleToADGroupColNum++;
                            wsRoleToADGroup.Cell(wsRoleToADGroupRowNum, wsRoleToADGroupColNum).Value = roleToADGroupDTO.ADGroupDTOFK.IsArchive == true ? "Да" : "";

                            wsRoleToADGroupRowNum++;
                        }
                    }


                    if (roleVMDTO.RoleToDepartmentDTOs != null)
                    {
                        foreach (RoleToDepartmentDTO roleToDepartmentDTO in roleVMDTO.RoleToDepartmentDTOs)
                        {

                            wsRoleToDepartmentColNum = 1;

                            wsRoleToDepartment.Cell(wsRoleToDepartmentRowNum, wsRoleToDepartmentColNum).Value = roleToDepartmentDTO.Id.ToString();
                            wsRoleToDepartmentColNum++;
                            wsRoleToDepartment.Cell(wsRoleToDepartmentRowNum, wsRoleToDepartmentColNum).Value = roleToDepartmentDTO.RoleDTOFK.Id.ToString();
                            wsRoleToDepartmentColNum++;
                            wsRoleToDepartment.Cell(wsRoleToDepartmentRowNum, wsRoleToDepartmentColNum).Value = roleToDepartmentDTO.RoleDTOFK.Name;
                            wsRoleToDepartmentColNum++;
                            wsRoleToDepartment.Cell(wsRoleToDepartmentRowNum, wsRoleToDepartmentColNum).Value = roleToDepartmentDTO.RoleDTOFK.Description == null ? "" : roleToDepartmentDTO.RoleDTOFK.Description;
                            wsRoleToDepartmentColNum++;
                            wsRoleToDepartment.Cell(wsRoleToDepartmentRowNum, wsRoleToDepartmentColNum).Value = roleToDepartmentDTO.RoleDTOFK.IsArchive == true ? "Да" : "";

                            wsRoleToDepartmentColNum++;
                            wsRoleToDepartment.Cell(wsRoleToDepartmentRowNum, wsRoleToDepartmentColNum).Value = roleToDepartmentDTO.DepartmentDTOFK.Id.ToString();
                            wsRoleToDepartmentColNum++;
                            wsRoleToDepartment.Cell(wsRoleToDepartmentRowNum, wsRoleToDepartmentColNum).Value = roleToDepartmentDTO.DepartmentDTOFK.MesCode == null ? "" : roleToDepartmentDTO.DepartmentDTOFK.MesCode;
                            wsRoleToDepartmentColNum++;
                            wsRoleToDepartment.Cell(wsRoleToDepartmentRowNum, wsRoleToDepartmentColNum).Value = roleToDepartmentDTO.DepartmentDTOFK.Name;
                            wsRoleToDepartmentColNum++;
                            wsRoleToDepartment.Cell(wsRoleToDepartmentRowNum, wsRoleToDepartmentColNum).Value = roleToDepartmentDTO.DepartmentDTOFK.ShortName;
                            wsRoleToDepartmentColNum++;
                            wsRoleToDepartment.Cell(wsRoleToDepartmentRowNum, wsRoleToDepartmentColNum).Value = roleToDepartmentDTO.DepartmentDTOFK.IsArchive == true ? "Да" : "";

                            wsRoleToDepartmentRowNum++;
                        }
                    }

                    wsRoleRowNum++;
                }

                for (var j = 1; j <= wsRoleColNum; j++)
                    wsRole.Column(j).AdjustToContents();

                for (var j = 1; j <= wsRoleToDepartmentColNum; j++)
                    wsUserToRole.Column(j).AdjustToContents();

                for (var j = 1; j <= wsReportTemplateTypeTоRoleColNum; j++)
                    wsReportTemplateTypeTоRole.Column(j).AdjustToContents();

                for (var j = 1; j <= wsRoleToADGroupColNum; j++)
                    wsRoleToADGroup.Column(j).AdjustToContents();

                for (var j = 1; j <= wsRoleToADGroupColNum; j++)
                    wsRoleToDepartment.Column(j).AdjustToContents();


                wbook.SaveAs(fullfilepath);
                if (wbook != null)
                    wbook.Dispose();
            }
            return fullfilepath;
        }


        public async Task<Tuple<IXLWorksheet, int>> AddAllMesDepartmentToExcel(IEnumerable<MesDepartmentVMDTO>? topLevelList, IXLWorksheet? ws, int excelRowNum, int maxLevel)
        {

            if (topLevelList != null)
            {
                foreach (var topLevelItem in topLevelList)
                {
                    MesDepartmentVMDTO? parentDepartmentVMDTO = topLevelItem;
                   
                    int breakCount = 0;
                    while (parentDepartmentVMDTO != null && breakCount <= 100)
                    {
                        //if ((parentDepartmentVMDTO.DepLevel <= 0 || parentDepartmentVMDTO.DepLevel >= 16384))
                        //{ 
                        //int a = 3;
                        //}
                        //else
                            ws.Cell(excelRowNum, parentDepartmentVMDTO.DepLevel).Value = parentDepartmentVMDTO.ShortName;
                        parentDepartmentVMDTO = parentDepartmentVMDTO.DepartmentParentVMDTO;
                        breakCount++;
                    }


                    int j = maxLevel + 1;

                    ws.Cell(excelRowNum, j).Value = topLevelItem.DepLevel.ToString();
                    j++;
                    ws.Cell(excelRowNum, j).Value = topLevelItem.Id.ToString();
                    j++;
                    ws.Cell(excelRowNum, j).Value = topLevelItem.MesCode == null ? "" : topLevelItem.MesCode;
                    j++;
                    ws.Cell(excelRowNum, j).Value = topLevelItem.Name == null ? "" : topLevelItem.Name;
                    j++;
                    ws.Cell(excelRowNum, j).Value = topLevelItem.ShortName == null ? "" : topLevelItem.ShortName;
                    j++;
                    ws.Cell(excelRowNum, j).Value = topLevelItem.IsArchive == true ? "Да" : "";

                    excelRowNum++;

                    Tuple<IXLWorksheet, int> tmp = await AddAllMesDepartmentToExcel(topLevelItem.ChildrenDTO, ws, excelRowNum, maxLevel);
                    ws = tmp.Item1;
                    excelRowNum = tmp.Item2;
                }
            }
            return new Tuple<IXLWorksheet, int>(ws, excelRowNum);

        }

        public async Task<string> GenerateExcelMesDepartments(string filename, IEnumerable<MesDepartmentVMDTO>? mesDepartmentVMDTOList, int maxLevel)
        {
            string pathVar = (await _settingsRepository.GetByName("TempFilePath")).Value;
            string fullfilepath = System.IO.Path.Combine(pathVar, filename);

            using var wbook = new XLWorkbook();
            {

                var ws = wbook.AddWorksheet("MesDepartment");

                int excelRowNum = 1;
                int excelColNum = 1;
                
                for (int j = 1; j <= maxLevel; j++)
                {
                    ws.Cell(excelRowNum, j).Value = "Производство - Уровень " + j.ToString();
                }

                excelColNum = maxLevel + 1;

                ws.Cell(excelRowNum, excelColNum).Value = "Уровень";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "ИД (Id)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Код пр-ва (Code)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование (Name)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Сокр. наименование (ShortName)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "В архиве (IsArchive)";

                ws.Row(excelRowNum).Style.Font.SetBold(true);
                ws.Row(excelRowNum).Style.Fill.BackgroundColor = XLColor.LightCyan;

                excelRowNum++;

                Tuple<IXLWorksheet, int> tmp = await AddAllMesDepartmentToExcel(mesDepartmentVMDTOList, ws, excelRowNum, maxLevel);
                ws = tmp.Item1;

                for (var jjj = 1; jjj <= excelColNum; jjj++)
                    ws.Column(jjj).AdjustToContents();

                wbook.SaveAs(fullfilepath);
                if (wbook != null)
                    wbook.Dispose();
            }
            return fullfilepath;
        }

        public async Task<string> GenerateExcelMesNdoStocks(string filename, IEnumerable<MesNdoStocksDTO> data)
        {

            string pathVar = (await _settingsRepository.GetByName("TempFilePath")).Value;
            string fullfilepath = System.IO.Path.Combine(pathVar, filename);

            using var wbook = new XLWorkbook();
            {

                var ws = wbook.AddWorksheet("MesNdoStocks");

                int excelRowNum = 1;
                int excelColNum = 1;


                ws.Cell(excelRowNum, excelColNum).Value = "ИД записи (Id)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Ид тэга СИР (MesParamId)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Код тэга СИР (MesParam.Code)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Наименование тэга СИР (MesParam.Name)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время добавления записи (AddTime)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Ид пользователя (AddUserId)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Логин пользователя (User.Login)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "ФИО пользователя (User.UserName)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время значения (ValueTime)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Значение (Value)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Разность (ValueDifference)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "ИД экземпляра отчёта (ReportGuid)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "ИД записи в витрине SAP (SapNdoOutId)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время добавления в витрину (SapNdoOUT.AddTime)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Имя тэга в витрине (SapNdoOUT.TagName)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время значения в витрине (SapNdoOUT.ValueTime)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Значение в витрине (SapNdoOUT.Value)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Sap забрал значение (SapNdoOUT.SapGone)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время Sap забрал значение (SapNdoOUT.SapGoneTime)";

                excelRowNum = 2;
                foreach (MesNdoStocksDTO mesNdoStocksDTO in data)
                {
                    excelColNum = 1;

                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.Id.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.MesParamId.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.MesParamDTOFK.Code;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.MesParamDTOFK.Name == null ? "" : mesNdoStocksDTO.MesParamDTOFK.Name;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.AddTime.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.AddUserId.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.AddUserDTOFK.Login;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.AddUserDTOFK.UserName;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.ValueTime.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.Value.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.ValueDifference == null ? "" : mesNdoStocksDTO.ValueDifference.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.ReportGuid == null ? "" : mesNdoStocksDTO.ReportGuid.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.SapNdoOutId == null ? "" : mesNdoStocksDTO.SapNdoOutId.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.SapNdoOUTDTOFK == null ? "" : mesNdoStocksDTO.SapNdoOUTDTOFK.AddTime.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.SapNdoOUTDTOFK == null ? "" : mesNdoStocksDTO.SapNdoOUTDTOFK.TagName;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.SapNdoOUTDTOFK == null ? "" : mesNdoStocksDTO.SapNdoOUTDTOFK.ValueTime.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.SapNdoOUTDTOFK == null ? "" : mesNdoStocksDTO.SapNdoOUTDTOFK.Value.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.SapNdoOUTDTOFK == null ? "" : (mesNdoStocksDTO.SapNdoOUTDTOFK.SapGone == true ? "Да" : "");
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = mesNdoStocksDTO.SapNdoOUTDTOFK == null ? "" : (mesNdoStocksDTO.SapNdoOUTDTOFK.SapGoneTime == null ? "" : mesNdoStocksDTO.SapNdoOUTDTOFK.SapGoneTime.ToString());

                    excelRowNum++;
                }

                for (var j = 1; j <= excelColNum; j++)
                    ws.Column(j).AdjustToContents();
                wbook.SaveAs(fullfilepath);
                if (wbook != null)
                    wbook.Dispose();
            }
            return fullfilepath;
        }
        

        public async Task<string> GenerateExcelSapNdoOUT(string filename, IEnumerable<SapNdoOUTDTO> data)
        {

            string pathVar = (await _settingsRepository.GetByName("TempFilePath")).Value;
            string fullfilepath = System.IO.Path.Combine(pathVar, filename);

            using var wbook = new XLWorkbook();
            {

                var ws = wbook.AddWorksheet("SapNdoOUT");

                int excelRowNum = 1;
                int excelColNum = 1;


                ws.Cell(excelRowNum, excelColNum).Value = "ИД записи (Id)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Имя тэга (TagName)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время добавления записи (AddTime)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время значения (ValueTime)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Значение (Value)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Sap забрал значение (SapGone)";
                excelColNum++;
                ws.Cell(excelRowNum, excelColNum).Value = "Время Sap забрал значение (SapGoneTime)";

                excelRowNum = 2;
                foreach (SapNdoOUTDTO sapNdoOUTDTO in data)
                {
                    excelColNum = 1;

                    ws.Cell(excelRowNum, excelColNum).Value = sapNdoOUTDTO.Id.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapNdoOUTDTO.TagName;
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapNdoOUTDTO.AddTime.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapNdoOUTDTO.ValueTime.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapNdoOUTDTO.Value.ToString();
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapNdoOUTDTO.SapGone == true ? "Да" : "";
                    excelColNum++;
                    ws.Cell(excelRowNum, excelColNum).Value = sapNdoOUTDTO.SapGoneTime == null ? "" : sapNdoOUTDTO.SapGoneTime.ToString();

                    excelRowNum++;
                }

                for (var j = 1; j <= excelColNum; j++)
                    ws.Column(j).AdjustToContents();
                wbook.SaveAs(fullfilepath);
                if (wbook != null)
                    wbook.Dispose();
            }
            return fullfilepath;
        }
    }
}
