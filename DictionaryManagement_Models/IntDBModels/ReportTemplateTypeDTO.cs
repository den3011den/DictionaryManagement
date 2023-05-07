using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_Models.IntDBModels
{
    public class ReportTemplateTypeDTO
    {
        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "Код типа шаблона отчёта является обязательным для заполнения полем")]        
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование типа шаблона отчёта является обязательным для заполнения полем")]
        [Display(Name = "Наименование типа шаблона отчёта")]
        [MaxLength(100, ErrorMessage = "Наименование типа шаблона отчёта не может быть больше 100 символов")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }
    }
}
