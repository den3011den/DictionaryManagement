using DictionaryManagement_Business.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DictionaryManagement_Server.Controllers
{
    public class UploadFileController : Controller
    {

        private readonly ISettingsRepository _settingsRepository;

        public UploadFileController(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        [HttpPost("UploadFileController/UploadReportTemplateFile/{reportTemplateId}")]
        [RequestSizeLimit(60000000)]
        public async Task<IActionResult> UploadReportTemplateFile(IFormFile file, Guid reportTemplateId)
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
            try
            {

                await UploadTemplateFile(file, reportTemplateId, pathVar);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task UploadTemplateFile(IFormFile file, Guid reportTemplateGuid, string reportTemplatePath)
        {
            try
            {

                if (!User.Identity.IsAuthenticated)
                {
                    return;
                }
            }
            catch
            {
                return;
            }



            if (file != null && file.Length > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                var fullPath = Path.Combine(reportTemplatePath, reportTemplateGuid.ToString() + extension);
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite/*, FileShare.ReadWrite, 800000000*/))
                {
                    await file.CopyToAsync(fileStream);

                }
            }
        }
    }
}
