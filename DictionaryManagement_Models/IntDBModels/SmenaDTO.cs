using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace DictionaryManagement_Models.IntDBModels
{
    public class SmenaDTO
    {

        [Display(Name = "Ид записи")]        
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Наименование смены обязательно")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ид производства обязательно")]
        [Display(Name = "Ид производства")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Выбор производства обязателен")]
        [Display(Name = "Производство")]
        public MesDepartmentDTO DepartmentDTOFK { get; set; }
    
        [Display(Name = "Время начала смены")]
        [Required(ErrorMessage = "Время начала смены обязательно")]                
        public TimeSpan StartTime { get; set; }

        [NotMapped]
        [Display(Name = "Время начала смены")]
        [Required(ErrorMessage = "Время начала смены обязательно")]
        public DateTime StartTimeDateTime { get; set; }


        [Display(Name = "Длительность смены (ч.)")]
        [Required(ErrorMessage = "Длительность смены обязательна")]
        [Range(1, 24, ErrorMessage = "Продолжительность смены не может быть меньше одного часа и больше 24 часов")]
        public byte HoursDuration { get; set; }

        [Display(Name = "В архиве")]
        public bool? IsArchive { get; set; }
       
    }
}
