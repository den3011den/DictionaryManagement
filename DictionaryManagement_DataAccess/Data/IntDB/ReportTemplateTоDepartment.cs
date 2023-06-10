using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("ReportTemplateTоDepartment", Schema = "dbo")]
    public class ReportTemplateTоDepartment
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required]
        public string ReportTemplateId { get; set; }

        [ForeignKey("ReportTemplateId")]
        public ReportTemplate ReportTemplateFK { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public MesDepartment DepartmentFK { get; set; }

    }

}
