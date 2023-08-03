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
        [Required(ErrorMessage = "ИД обязателен")]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Наименование смены обязательно")]
        public string Name { get; set; }
        
        [Display(Name = "ИД производства")]
        [Required(ErrorMessage = "ИД производства обязательно")]
        public int DepartmentId { get; set; }

        [Display(Name = "Производство")]
        [Required(ErrorMessage = "Производство обязателено")]
        public MesDepartmentDTO DepartmentDTOFK { get; set; }

        [Display(Name = "Время начала смены")]
        [Required(ErrorMessage = "Время начала смены обязательно")]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "Длительность смены (ч.)")]
        [Required(ErrorMessage = "Длительность смены обязательна")]
        public byte HoursDuration { get; set; }

        [Display(Name = "В архиве")]
        public bool? IsArchive { get; set; }
       
    }
}
