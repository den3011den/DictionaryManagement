using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DictionaryManagement_Models.IntDBModels
{
    public class ReportTemplateTоDepartmentDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "ИД обязателен")]
        public string Id { get; set; }

        [Required(ErrorMessage = "ИД шаблона отчёта обязателен")]
        [Display(Name = "Ид шаблона отчёта")]
        public string ReportTemplateId { get; set; }

        [Required(ErrorMessage = "Выбор шаблона отчёта обязателен")]
        [Display(Name = "Шаблон отчёта")]
        public ReportTemplateDTO ReportTemplateDTOFK { get; set; }

        [Required(ErrorMessage = "ИД производства обязателен")]
        [Display(Name = "Ид производства")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Выбор производства обязателен")]
        [Display(Name = "Производство")]
        public MesDepartmentDTO DepartmentDTOFK { get; set; }

    }
}

