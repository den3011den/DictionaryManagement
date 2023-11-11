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
    }
}
