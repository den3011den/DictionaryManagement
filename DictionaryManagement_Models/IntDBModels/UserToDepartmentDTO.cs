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
    public class UserToDepartmentDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "ИД обязателен")]
        public string Id { get; set; }

        [Required(ErrorMessage = "ИД пользователя обязателен")]
        [Display(Name = "Ид пользователя")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Выбор пользователя обязателен")]
        [Display(Name = "Прользователь")]
        public UserDTO UserDTOFK { get; set; }

        [Required(ErrorMessage = "ИД производтсва обязателен")]
        [Display(Name = "Ид производства")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Выбор производства обязателен")]
        [Display(Name = "Производство")]
        public MesDepartmentDTO DepartmentDTOFK { get; set; }

    }
}

