using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    
    [Table("Version", Schema = "dbo")]
    public class Version
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Column("Version")]
        public string? version { get; set; }

    }
}
