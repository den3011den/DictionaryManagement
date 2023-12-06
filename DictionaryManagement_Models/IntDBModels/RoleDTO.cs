using System.ComponentModel.DataAnnotations;

namespace DictionaryManagement_Models.IntDBModels
{
    public class RoleDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "ИД обязателен")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Наименование может быть от 1 до 100 символов")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; } = false;

    }
}

