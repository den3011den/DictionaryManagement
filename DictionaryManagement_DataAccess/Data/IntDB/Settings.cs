using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("Settings", Schema = "dbo")]
    public class Settings
    {
        [Key]
        [Required]     
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(300)]        
        public string Description { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public string Value { get; set; } = string.Empty;


    }
}
