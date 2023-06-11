﻿using DictionaryManagement_Common;
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
    public interface IReportTemplateRepository
    {
        public Task<ReportTemplateDTO> GetById(string id);
        public Task<IEnumerable<ReportTemplateDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);        
        public Task<ReportTemplateDTO> Update(ReportTemplateDTO objDTO);
        public Task<ReportTemplateDTO> Create(ReportTemplateDTO objectToAddDTO);
        public Task<int> Delete(string id, UpdateMode updateMode = UpdateMode.Update);
        public Task<ReportTemplateDTO> GetByTemplateFileName(string templateFileName = "");
    }
}
