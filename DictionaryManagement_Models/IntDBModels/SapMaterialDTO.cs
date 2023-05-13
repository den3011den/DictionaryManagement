using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_Models.IntDBModels
{
    public class SapMaterialDTO
    {
        [Display(Name = "Код записи")]
        [Required(ErrorMessage = "Код материала SAP является обязательным для заполнения полем")]
        [MaxLength(100, ErrorMessage = "Код материала SAP не может быть больше 100 символов")]
        public string Id { get; set; } = string.Empty;
       
        [Required(ErrorMessage = "Наименование материала SAP является обязательным для заполнения полем")]
        [Display(Name = "Наименование материала SAP")]
        [MaxLength(250, ErrorMessage = "Наименование материала SAP не может быть больше 250 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Сокращённое наименование материала SAP является обязательным для заполнения полем")]
        [Display(Name = "Сокращённое наименование материала SAP")]
        [MaxLength(100, ErrorMessage = "Сокращённое наименование материала SAP не может быть больше 100 символов")]
        public string ShortName { get; set; } = string.Empty;


        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }
    }
}
