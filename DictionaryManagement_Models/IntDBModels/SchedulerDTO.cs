using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DictionaryManagement_Models.IntDBModels
{
    public class SchedulerDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "ИД записи обязательно")]
        public int Id { get; set; }

        [Display(Name = "Наименование модуля")]
        [Required(ErrorMessage = "Наименование модуля обязательно")]
        public string ModuleName { get; set; }

        [Display(Name = "Время старта задания")]
        [Required(ErrorMessage = "Время старта задания обязательно")]
        public TimeSpan StartTime { get; set; }

        [NotMapped]
        [Display(Name = "Время старта задания")]
        [Required(ErrorMessage = "Время старта задания обязательно")]
        public DateTime StartTimeDateTime { get; set; }

        [Display(Name = "Время последнего выполнения")]
        public DateTime? LastExecuted { get; set; } = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
    }
}
