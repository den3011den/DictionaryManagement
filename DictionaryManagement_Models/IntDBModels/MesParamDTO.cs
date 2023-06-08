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
    public class MesParamDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "ИД обязателен")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Код обязателен для заполнения")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Код может быть от 1 до 100 символов")]        
        [Display(Name = "Код СИР")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Наименование тэга СИР обязатенльно для заполнения")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Наименование может быть от 3 до 250 символов")]
        [Display(Name = "Наименование")]
        public string? Name { get; set; }
        
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Описание может быть от 3 до 100 символов")]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Ид типа источника параметра")]
        public int? MesParamSourceType { get; set; }

        [Display(Name = "Источник")]
        public MesParamSourceTypeDTO? MesParamSourceTypeDTOFK { get; set; }

        [Display(Name = "Тэг источника")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Тэг источника может быть от 3 до 300 символов")]
        public string MesParamSourceLink { get; set; }

        [Display(Name = "Ид производства")]
        public int? DepartmentId { get; set; }

        [Display(Name = "Производство")]
        public MesDepartmentDTO? MesDepartmentDTOFK { get; set; }

        [Display(Name = "Ид источника SAP")]
        public int? SapEquipmentIdSource { get; set; }

        [Display(Name = "Источник SAP")]
        public SapEquipmentDTO? SapEquipmentSourceDTOFK { get; set; }

        [Display(Name = "Ид приёмника SAP")]
        public int? SapEquipmentIdDest { get; set; }

        [Display(Name = "Приёмник SAP")]
        public SapEquipmentDTO? SapEquipmentDestDTOFK { get; set; }

        [Display(Name = "Ид материала MES")]
        public int? MesMaterialId { get; set; }

        [Display(Name = "Материал MES")]
        public MesMaterialDTO? MesMaterialDTOFK { get; set; }

        [Display(Name = "Ид материала SAP")]
        public int? SapMaterialId { get; set; }

        [Display(Name = "Материал SAP")]
        public SapMaterialDTO? SapMaterialDTOFK { get; set; }

        [Display(Name = "Ид ед.изм. MES")]
        public int? MesUnitOfMeasureId { get; set; }

        [Display(Name = "Ед.изм. MES")]
        public MesUnitOfMeasureDTO? MesUnitOfMeasureDTOFK { get; set; }

        [Display(Name = "Ид ед.изм. SAP")]
        public int? SapUnitOfMeasureId { get; set; }

        [Display(Name = "Ед.изм. SAP")]
        public SapUnitOfMeasureDTO? SapUnitOfMeasureDTOFK { get; set; }

        [Display(Name = "Глубина опроса (в днях)")]
        public int? DaysRequestInPast { get; set; } = 45;

        [Display(Name = "Передавать в SAP")]
        public bool? NeedWriteToSap { get; set; }

        [Display(Name = "Читать из SAP")]
        public bool? NeedReadFromSap { get; set; }

        [Display(Name = "Читать из MES")]
        public bool? NeedReadFromMes { get; set; }

        [Display(Name = "Передавать в MES")]
        public bool? NeedWriteToMes { get; set; }

        [Display(Name = "Параметр НДО")]
        public bool? IsNdo { get; set; }

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }



    }
}

