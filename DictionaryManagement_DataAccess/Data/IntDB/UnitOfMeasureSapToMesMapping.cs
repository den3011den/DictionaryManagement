using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("UnitOfMeasureSapToMesMapping", Schema = "dbo")]
    public class UnitOfMeasureSapToMesMapping
    {
       
        [Key, Column(Order = 0)]
        public int SapUnitId { get; set; }

        [ForeignKey("SapUnitId")]
        public SapUnitOfMeasure SapUnitOfMeasure { get; set; }

        [Key, Column(Order = 1)]
        public int MesUnitId { get; set; }

        [ForeignKey("MesUnitId")]
        public MesUnitOfMeasure MesUnitOfMeasure { get; set; } 

        [Required]
        [Range(0.0001, 1000000000, ErrorMessage = "Значение должно быть между {1} and {2}")]        
        public decimal SapToMesTransformKoef { get; set; } = decimal.One;

        
    }

}
