using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_Models.IntDBModels
{
    public class MesParamSourceTypeDTO
    {
        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "Код типа параметра является обязательным для заполнения полем")]        
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование типа параметра является обязательным для заполнения полем")]
        [Display(Name = "Наименование типа параметра")]
        [MaxLength(250, ErrorMessage = "Наименование типа параметра не может быть больше 250 символов")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; } = false;

        [NotMapped]
        public string ToStringValue
        {
            get
            {
                return Name;
            }
            set
            {
                ToStringValue = value;
            }
        }

        public override string ToString()
        {
            ToStringValue = $"{Name}";
            return ToStringValue;
        }

    }
}
