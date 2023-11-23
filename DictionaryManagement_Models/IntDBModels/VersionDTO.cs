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
    public class VersionDTO
    {

        [Display(Name = "ИД записи")] 
        [Required(ErrorMessage = "ИД обязателен")]
        public int Id { get; set; }

        [Display(Name = "Версия БД")]
        [Required(ErrorMessage = "Версия БД обязательна")]
        [MaxLength(20, ErrorMessage = "Значение не должно быть больше 20-ти символов")]
        public string version { get; set; }
    }
}

