using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("ReportTemplate", Schema = "dbo")]
    public class ReportTemplate
    {

        [Key]        
        [Required]
        public string Id { get; set; }

        [Required]
        public DateTime AddTime { get; set; }        

        public string? Description { get; set; }

        [Required]
        public string AddUserId { get; set; }
        
        [ForeignKey("AddUserId")]
        public User AddUserFK { get; set; }

        [Required]
        public int ReportTemplateTypeId { get; set; }

        [ForeignKey("ReportTemplateTypeId")]
        public ReportTemplateType ReportTemplateTypeFK { get; set; }



        [Required]
        public int DestDataTypeId { get; set; }

        [ForeignKey("DestDataTypeId")]
        public ReportTemplateType DestDataTypeFK { get; set; }

        [Required]
        public string TemplateFileName { get; set; }

        public bool IsArchive { get; set; }
    }

}
