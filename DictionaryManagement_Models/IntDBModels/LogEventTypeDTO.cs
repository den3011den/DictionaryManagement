﻿using System.ComponentModel.DataAnnotations;

namespace DictionaryManagement_Models.IntDBModels
{
    public class LogEventTypeDTO
    {
        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "Код типа собылия является обязательным для заполнения полем")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование типа собылия является обязательным для заполнения полем")]
        [Display(Name = "Наименование типа события")]
        [MaxLength(100, ErrorMessage = "Наименование типа собылия не может быть больше 100 символов")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }
    }
}
