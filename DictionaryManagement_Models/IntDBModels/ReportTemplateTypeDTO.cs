﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_Models.IntDBModels
{
    public class ReportTemplateTypeDTO
    {
        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "Код типа шаблона отчёта является обязательным для заполнения полем")]        
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование типа шаблона отчёта является обязательным для заполнения полем")]
        [Display(Name = "Наименование типа шаблона отчёта")]
        [MaxLength(100, ErrorMessage = "Наименование типа шаблона отчёта не может быть больше 100 символов")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }

        [Display(Name = "Требуется авторасчёт")]
        public bool? NeedAutoCalc { get; set; }

        [NotMapped]
        [Display(Name = "Чтение")]
        public bool CanDownload { get; set; }

        [NotMapped]
        [Display(Name = "Запись")]
        public bool CanUpload { get; set; }

        [NotMapped]
        [Display(Name = "Ид записи")]
        public string ToStringId
        {
            get
            {
                return Id.ToString();
            }
            set
            {
                ToStringId = value;
            }
        }

    }
}
