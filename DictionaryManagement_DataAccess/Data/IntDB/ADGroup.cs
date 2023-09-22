using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("ADGroup", Schema = "dbo")]
    public class ADGroup
    {

        [Key]        
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }        

        public bool IsArchive { get; set; } = false;

    }

}
