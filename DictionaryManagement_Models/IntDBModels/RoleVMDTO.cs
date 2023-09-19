using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

/* ViewModel для списка ролей для отображения в справочнике и управления привязками к шаблонам отчётов и пользователям */

namespace DictionaryManagement_Models.IntDBModels
{
    public class RoleVMDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "ИД обязателен")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Наименование может быть от 1 до 100 символов")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }
                        
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [NotMapped]
        public IEnumerable<UserToRoleDTO>? UserToRoleDTOs { get; set; }
        [NotMapped]
        public IEnumerable<ReportTemplateTypeTоRoleDTO>? ReportTemplateTypeTоRoleDTOs { get; set; }

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; } = false;

    }
}

