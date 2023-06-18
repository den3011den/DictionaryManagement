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
    public class ReportTemplateDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "ИД обязателен")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Время добавления обязательно")]        
        [Display(Name = "Время добавления")]
        public DateTime AddTime { get; set; }

        [Required(ErrorMessage = "ИД добавившего пользователя обязательно")]
        [Display(Name = "Ид добавившего пользователя")]
        public Guid AddUserId { get; set; }
        
        [Required(ErrorMessage = "Выбор пользователя обязателен")]
        [Display(Name = "Пользователь")]
        public UserDTO AddUserDTOFK { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; } = "Шаблон типа: \"\" c вых данными: \"\" для производства: \"\"";

        [Required(ErrorMessage = "Ид типа отчёта обязательно")]
        [Display(Name = "Ид типа отчёта")]
        public int ReportTemplateTypeId { get; set; }

        [Required(ErrorMessage = "Выбор типа отчёта обязателен")]
        [Display(Name = "Тип отчёта")]
        public ReportTemplateTypeDTO ReportTemplateTypeDTOFK { get; set; }

        [Required(ErrorMessage = "Ид типа данных обязательно")]
        [Display(Name = "Ид типа данных")]
        public int DestDataTypeId { get; set; }

        [Required(ErrorMessage = "Выбор типа данных обязателен")]
        [Display(Name = "Тип данных")]
        public DataTypeDTO DestDataTypeDTOFK { get; set; }

        [Required(ErrorMessage = "Ид производства обязательно")]
        [Display(Name = "Ид производства")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Выбор производства обязателен")]
        [Display(Name = "Производство")]
        public MesDepartmentDTO MesDepartmentDTOFK { get; set; }

        [Display(Name = "Имя файла")]
        [Required(ErrorMessage = "Выбор файла обязателен")]
        public string TemplateFileName { get; set; }

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }

    }
}

