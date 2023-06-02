using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DictionaryManagement_Models.IntDBModels
{
    public class MesDepartmentDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "Код обязателен для заполнения")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Код")]
        public int MesCode { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Сокр. наименование")]
        public string ShortName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Ид родителя")]
        public int ParentDepartmentId { get; set; }

        [Required]
        [Display(Name = "Родитель")]
        public MesDepartmentDTO? DepartmentParentDTO { get; set; }

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }
    }
}
}
