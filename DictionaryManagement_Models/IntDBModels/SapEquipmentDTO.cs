using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryManagement_Models.IntDBModels
{
    public class SapEquipmentDTO
    {
        [Display(Name = "Код записи")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Код завода SAP является обязательным для заполнения полем")]
        [Display(Name = "Код завода SAP")]
        [MaxLength(100, ErrorMessage = "Код завода SAP не может быть больше 100 символов")]
        public string ErpPlantId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Код ресурса/склада SAP является обязательным для заполнения полем")]
        [Display(Name = "Код ресурса/склада SAP")]
        [MaxLength(100, ErrorMessage = "Код ресурса/склада SAP не может быть больше 100 символов")]
        public string ErpId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Наименование ресурса/склада SAP является обязательным для заполнения полем")]
        [Display(Name = "Наименование ресурса/склада SAP")]
        [MaxLength(100, ErrorMessage = "Наименование ресурса/склада SAP не может быть больше 250 символов")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Является складом")]
        public bool IsWarehouse { get; set; } = false;

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }

        [NotMapped]
        public string ToStringValue
        {
            get
            {
                return ErpPlantId + "|" + ErpId + " " + Name;
            }
            set
            {
                ToStringValue = value;
            }
        }

        public override string ToString()
        {
            return ErpPlantId + "|" + ErpId + " " + Name; ;
        }

        [NotMapped]
        public string ToStringErpPlantIdErpIdName
        {
            get
            {

                return ErpPlantId + "|" + ErpId + " " + Name;
            }
            set
            {
                ToStringErpPlantIdErpIdName = value;
            }
        }

    }
}
