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
    public class RoleToDepartmentDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "ИД обязателен")]
        public int Id { get; set; }

        [Required(ErrorMessage = "ИД роли обязателен")]
        [Display(Name = "Ид роли")]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage = "Выбор роли обязателен")]
        [Display(Name = "Роль")]
        public RoleDTO RoleDTOFK { get; set; }

        [Required(ErrorMessage = "ИД производтсва обязателен")]
        [Display(Name = "Ид производства")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Выбор производства обязателен")]
        [Display(Name = "Производство")]
        public MesDepartmentDTO DepartmentDTOFK { get; set; }

    }
}

