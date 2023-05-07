using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_Models.IntDBModels
{
    public class ErrorCriterionDTO
    {
        [Display(Name = "Код записи")]
        [Required(ErrorMessage = "Код критерия ошибки является обязательным для заполнения полем")]        
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование критерия ошибки является обязательным для заполнения полем")]
        [Display(Name = "Наименование критерия ошибки")]
        [MaxLength(250, ErrorMessage = "Наименование критерия ошибки не может быть больше 250 символов")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }
    }
}
