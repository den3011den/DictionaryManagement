﻿using DictionaryManagement_Business.Repository;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_Models.IntDBModels;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryManagement_Server.Controllers
{
    public class DownloadFileController : Controller
    {

        private readonly ISettingsRepository _settingsRepository;
        private readonly IReportTemplateRepository _reportTemplateRepository;
        private readonly IReportEntityRepository _reportEntityRepository;
        private readonly IReportTemplateTypeRepository _reportTemplateTypeRepository;
        private readonly IAuthorizationControllersRepository _authorizationControllersRepository;

        public DownloadFileController(ISettingsRepository settingsRepository, IReportTemplateRepository reportTemplateRepository,
            IReportEntityRepository reportEntityRepository, IReportTemplateTypeRepository reportTemplateTypeRepository,
            IAuthorizationControllersRepository authorizationControllersRepository)
        {
            _settingsRepository = settingsRepository;
            _reportTemplateRepository = reportTemplateRepository;
            _reportEntityRepository = reportEntityRepository;
            _reportTemplateTypeRepository = reportTemplateTypeRepository;
            _authorizationControllersRepository = authorizationControllersRepository;
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
                else
                {
                    if (!(await _authorizationControllersRepository.CurrentUserIsInAdminRoleByLogin(User.Identity.Name, SD.MessageBoxMode.Off)))
                    {
                        return StatusCode(401, "Вы не входите в группу " + SD.AdminRoleName + ". Доступ запрещён");
                    }
                }
            }
            catch
            {
                return StatusCode(401, "Не удалось проверить авторизацию. Вы не авторизованы. Доступ запрещён. Возможно авторизация отключена.");
            }


            string pathVar = (await _settingsRepository.GetByName("ReportTemplatePath")).Value;
            ReportTemplateDTO foundTemplate = await _reportTemplateRepository.GetById(reportTemplateId);
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
                else
                {
                    if (!(await _authorizationControllersRepository.CurrentUserIsInAdminRoleByLogin(User.Identity.Name, SD.MessageBoxMode.Off)))
                    {
                        return StatusCode(401, "Вы не входите в группу " + SD.AdminRoleName + ". Доступ запрещён");
                    }
                }
            }
            catch
            {
                return StatusCode(401, "Не удалось проверить авторизацию. Вы не авторизованы. Доступ запрещён. Возможно авторизация отключена.");
            }


            string pathVar = (await _settingsRepository.GetByName("ReportDownloadPath")).Value;
            ReportEntityDTO foundEntity = await _reportEntityRepository.GetById(reportEntityId);
            string fileName = foundEntity.DownloadReportFileName;
            string file = System.IO.Path.Combine(pathVar, fileName);
            var extension = Path.GetExtension(fileName);
            if (System.IO.File.Exists(file))
            {
                try
                {
                    var repTmplTypeDTO = await _reportTemplateTypeRepository.Get(foundEntity.ReportTemplateDTOFK.ReportTemplateTypeId);
                    var forFileName = ("Download_" + repTmplTypeDTO.Name + "_"
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
                else
                {
                    if (!(await _authorizationControllersRepository.CurrentUserIsInAdminRoleByLogin(User.Identity.Name, SD.MessageBoxMode.Off)))
                    {
                        return StatusCode(401, "Вы не входите в группу " + SD.AdminRoleName + ". Доступ запрещён");
                    }
                }

            }
            catch
            {
                return StatusCode(401, "Не удалось проверить авторизацию. Вы не авторизованы. Доступ запрещён. Возможно авторизация отключена.");
            }



            string pathVar = (await _settingsRepository.GetByName("ReportUploadPath")).Value;
            ReportEntityDTO foundEntity = await _reportEntityRepository.GetById(reportEntityId);
            //string fileName = foundEntity.UploadReportFileName;
            // в шестёрке решили в UploadReportFileName сохранять имя загружаемого пользователем файла
            // теперь приходится брать реально храняшееся имя файла из DownloadReportFileName
            string fileName = foundEntity.DownloadReportFileName;
            string file = System.IO.Path.Combine(pathVar, fileName);
            var extension = Path.GetExtension(fileName);
            if (System.IO.File.Exists(file))
            {
                try
                {
                    var reportTemptateTypeDTO = await _reportTemplateTypeRepository.Get(foundEntity.ReportTemplateDTOFK.ReportTemplateTypeId);
                    var forFileName = ("Upload_" + reportTemptateTypeDTO.Name + "_"
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

        [HttpGet("DownloadFileController/SimpleExcelExport/{filename}")]
        [RequestSizeLimit(60000000)]
        public async Task<IActionResult> SimpleExcelExport(string filename)
        {

            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "Вы не авторизованы. Доступ запрещён");
                }
                else
                {
                    if (!(await _authorizationControllersRepository.CurrentUserIsInAdminRoleByLogin(User.Identity.Name, SD.MessageBoxMode.Off)))
                    {
                        return StatusCode(401, "Вы не входите в группу " + SD.AdminRoleName + ". Доступ запрещён");
                    }
                }
            }
            catch
            {
                return StatusCode(401, "Не удалось проверить авторизацию. Вы не авторизованы. Доступ запрещён. Возможно авторизация отключена.");
            }

            string pathVar = (await _settingsRepository.GetByName("TempFilePath")).Value;
            string file = System.IO.Path.Combine(pathVar, filename);
            if (System.IO.File.Exists(file))
            {
                try
                {
                    var forFileName = filename.Replace(":", "_").Replace(",", "_").Replace("\"", "_").Replace("\'", "_");
                    return File(new FileStream(file, FileMode.Open), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", forFileName /*+ extension*/);
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
