﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_Models.IntDBModels
{
    public class SettingsDTO
    {
        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "Код настройки является обязательным для заполнения полем")]        
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование настройки является обязательным для заполнения полем")]
        [Display(Name = "Наименование настройки")]
        [MaxLength(100, ErrorMessage = "Наименование настройки не может быть больше 100 символов")]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Описание настройки")]
        [MaxLength(300, ErrorMessage = "Описание настройки не может быть больше 300 символов")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Значение настройки")]        
        public string Value { get; set; } = string.Empty;

    }
}
