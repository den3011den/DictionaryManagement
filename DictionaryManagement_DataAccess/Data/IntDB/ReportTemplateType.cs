using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("ReportTemplateType", Schema = "dbo")]
    public class ReportTemplateType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;

        public bool? NeedAutoCalc { get; set; }

        public bool IsArchive { get; set; }

    }
}
