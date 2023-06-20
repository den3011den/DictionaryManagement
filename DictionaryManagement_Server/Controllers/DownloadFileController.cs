using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Models.IntDBModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DictionaryManagement_Server.Controllers
{
    public class DownloadFileController : Controller
    {

        private readonly ISettingsRepository _settingsRepository;
        private readonly IReportTemplateRepository _reportTemplateRepository;

        public DownloadFileController(ISettingsRepository settingsRepository, IReportTemplateRepository reportTemplateRepository)
        {
            _settingsRepository = settingsRepository;
            _reportTemplateRepository = reportTemplateRepository;
        }
        
        [HttpGet("DownloadFileController/DownloadReportTemplateFile/{reportTemplateId}")]
        [RequestSizeLimit(60000000)]
        public async Task<IActionResult> DownloadReportTemplateFile(Guid reportTemplateId)
        {

            string pathVar = _settingsRepository.GetByName("ReportTemplatePath").GetAwaiter().GetResult().Value;
            ReportTemplateDTO foundTemplate = _reportTemplateRepository.GetById(reportTemplateId).GetAwaiter().GetResult();
            string fileName = foundTemplate.TemplateFileName;
            string description = foundTemplate.Description.Replace("\"","_").Replace(" ", "_").Trim();  
            string file = System.IO.Path.Combine(pathVar, fileName);
            var extension = Path.GetExtension(fileName);
            if (System.IO.File.Exists(file))
            {
                try
                {
                    return File(new FileStream(file, FileMode.Open), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", description + extension);
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
