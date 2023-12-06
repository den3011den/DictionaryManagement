using System.ComponentModel.DataAnnotations;

namespace DictionaryManagement_Models.IntDBModels
{
    public class DataSourceDTO
    {
        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "Код источника данных является обязательным для заполнения полем")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование источника данных является обязательным для заполнения полем")]
        [Display(Name = "Наименование источника данных")]
        [MaxLength(250, ErrorMessage = "Наименование источника данных не может быть больше 250 символов")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }
    }
}
