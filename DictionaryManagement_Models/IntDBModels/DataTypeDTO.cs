using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_Models.IntDBModels
{
    public class DataTypeDTO
    {
        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "Код вида данных является обязательным для заполнения полем")]        
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование вида данных является обязательным для заполнения полем")]
        [Display(Name = "Наименование вида данных")]
        [MaxLength(250, ErrorMessage = "Наименование вида данных не может быть больше 250 символов")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }
    }
}
