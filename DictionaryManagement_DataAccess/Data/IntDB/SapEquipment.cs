using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("SapEquipment", Schema = "dbo")]
    public class SapEquipment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ErpPlantId { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string ErpId { get; set; } = string.Empty;

        [Required]
        [MaxLength(250)]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
        public bool IsArchive { get; set; }
    }
}
