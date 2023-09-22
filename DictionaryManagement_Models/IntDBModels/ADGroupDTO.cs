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
    public class ADGroupDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "ИД обязателен")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Наименование может быть от 1 до 300 символов")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }
                        
        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; } = false;

    }
}

