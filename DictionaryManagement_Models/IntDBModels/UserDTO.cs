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
    public class UserDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "ИД обязателен")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Логин может быть от 1 до 100 символов")]        
        [Display(Name = "Логин")]
        public string Login { get; set; }
        
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Наименование может быть от 3 до 250 символов")]
        [Display(Name = "Наименование")]
        public string? UserName { get; set; }
                
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }

    }
}

