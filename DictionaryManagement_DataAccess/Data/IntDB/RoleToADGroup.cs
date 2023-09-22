using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("RoleToADGroup", Schema = "dbo")]
    public class RoleToADGroup
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role RoleFK { get; set; }

        [Required]
        public Guid ADGroupId { get; set; }

        [ForeignKey("ADGroupId")]
        public ADGroup ADGroupFK { get; set; }

    }

}
