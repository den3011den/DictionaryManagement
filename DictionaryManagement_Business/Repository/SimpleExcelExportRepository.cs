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
    }
}
