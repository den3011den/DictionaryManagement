using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Models.IntDBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DictionaryManagement_Server.Controllers
{
    public class DownloadFileController : Controller
    {

        private readonly ISettingsRepository _settingsRepository;
        private readonly IReportTemplateRepository _reportTemplateRepository;
        private readonly IReportEntityRepository _reportEntityRepository;
        private readonly IReportTemplateTypeRepository _reportTemplateTypeRepository;

        public DownloadFileController(ISettingsRepository settingsRepository, IReportTemplateRepository reportTemplateRepository,
            IReportEntityRepository reportEntityRepository, IReportTemplateTypeRepository reportTemplateTypeRepository)
        {
            _settingsRepository = settingsRepository;
            _reportTemplateRepository = reportTemplateRepository;
            _reportEntityRepository = reportEntityRepository;
            _reportTemplateTypeRepository = reportTemplateTypeRepository;
        }

        [HttpGet("DownloadFileController/DownloadReportTemplateFile/{reportTemplateId}")]
        [RequestSizeLimit(60000000)]
        public async Task<IActionResult> DownloadReportTemplateFile(Guid reportTemplateId)
        {

            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "Вы не авторизованы. Доступ запрещён");
                }

            }
            catch
            {
                return StatusCode(401, "Не удалось проверить авторизацию. Вы не авторизованы. Доступ запрещён. Возможно авторизация отключена.");
            }


            string pathVar = _settingsRepository.GetByName("ReportTemplatePath").GetAwaiter().GetResult().Value;
            ReportTemplateDTO foundTemplate = _reportTemplateRepository.GetById(reportTemplateId).GetAwaiter().GetResult();
            string fileName = foundTemplate.TemplateFileName;
            string file = System.IO.Path.Combine(pathVar, fileName);
            var extension = Path.GetExtension(fileName);
            if (System.IO.File.Exists(file))
            {
                try
                {
                    var forFileName = "Template_" + foundTemplate.ReportTemplateTypeDTOFK.Name + "_"
                        + foundTemplate.MesDepartmentDTOFK.ShortName + "_" + fileName
                        .Replace(":", "_").Replace(",", "_").Replace("\"", "_").Replace("\'", "_");
                    return File(new FileStream(file, FileMode.Open), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", forFileName /*+ extension*/);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            return StatusCode(500, "Файл " + file + " не найден");
        }

        [HttpGet("DownloadFileController/DownloadReportEntityDownloadFile/{reportEntityId}")]
        [RequestSizeLimit(60000000)]
        public async Task<IActionResult> DownloadReportEntityDownloadFile(Guid reportEntityId)
        {

            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "Вы не авторизованы. Доступ запрещён");
                }

            }
            catch
            {
                return StatusCode(401, "Не удалось проверить авторизацию. Вы не авторизованы. Доступ запрещён. Возможно авторизация отключена.");
            }


            string pathVar = _settingsRepository.GetByName("ReportDownloadPath").GetAwaiter().GetResult().Value;
            ReportEntityDTO foundEntity = _reportEntityRepository.GetById(reportEntityId).GetAwaiter().GetResult();
            string fileName = foundEntity.DownloadReportFileName;
            string file = System.IO.Path.Combine(pathVar, fileName);
            var extension = Path.GetExtension(fileName);
            if (System.IO.File.Exists(file))
            {
                try
                {
                    var forFileName = ("Download_" + _reportTemplateTypeRepository.Get(foundEntity.ReportTemplateDTOFK.ReportTemplateTypeId).GetAwaiter().GetResult().Name + "_"
                        + foundEntity.DownloadUserDTOFK.UserName
                        + "_" + foundEntity.DownloadTime.ToString() + "_"
                        + fileName)
                        .Replace(":", "_").Replace(",", "_").Replace("\"", "_").Replace("\'", "_");
                    return File(new FileStream(file, FileMode.Open), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", forFileName /*+ extension*/);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            return StatusCode(500, "Файл " + file + " не найден");
        }

        [HttpGet("DownloadFileController/DownloadReportEntityUploadFile/{reportEntityId}")]
        [RequestSizeLimit(60000000)]
        public async Task<IActionResult> DownloadReportEntityUploadFile(Guid reportEntityId)
        {

            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "Вы не авторизованы. Доступ запрещён");
                }
            }
            catch
            {
                return StatusCode(401, "Не удалось проверить авторизацию. Вы не авторизованы. Доступ запрещён. Возможно авторизация отключена.");
            }



            string pathVar = _settingsRepository.GetByName("ReportUploadPath").GetAwaiter().GetResult().Value;
            ReportEntityDTO foundEntity = _reportEntityRepository.GetById(reportEntityId).GetAwaiter().GetResult();
            string fileName = foundEntity.UploadReportFileName;
            string file = System.IO.Path.Combine(pathVar, fileName);
            var extension = Path.GetExtension(fileName);
            if (System.IO.File.Exists(file))
            {
                try
                {
                    var forFileName = ("Upload_" + _reportTemplateTypeRepository.Get(foundEntity.ReportTemplateDTOFK.ReportTemplateTypeId).GetAwaiter().GetResult().Name + "_"
                        + foundEntity.UploadUserDTOFK.UserName
                        + "_" + foundEntity.UploadTime.ToString() + "_"
                        + fileName)
                        .Replace(":", "_").Replace(",", "_").Replace("\"", "_").Replace("\'", "_");
                    return File(new FileStream(file, FileMode.Open), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", forFileName/* + extension*/);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            return StatusCode(500, "Файл " + file + " не найден");
        }

    }
}
