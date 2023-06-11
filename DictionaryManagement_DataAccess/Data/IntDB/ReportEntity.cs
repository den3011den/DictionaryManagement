using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("ReportEntity", Schema = "dbo")]
    public class ReportEntity
    {

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string ReportTemplateId { get; set; }
        [ForeignKey("ReportTemplateId")]
        public ReportTemplate ReportTemplateFK { get; set; }

        public DateTime? ReportTimeStart { get; set; }

        public DateTime? ReportTimeEnd { get; set; }

        public string? ReportDepartmentId { get; set; }
        [ForeignKey("ReportDepartmentId")]
        public MesDepartment? ReportDepartmentFK { get; set; }

        public DateTime? DownloadTime { get; set; }

        public string DownloadUserId { get; set; }
        [ForeignKey("DownloadUserId")]
        public User DownloadUserFK { get; set; }

        public string? DownloadReportFileName { get; set; }

        public bool? DownloadSuccessFlag { get; set; }

        public DateTime? UploadTime { get; set; }

        public string? UploadUserId { get; set; }
        [ForeignKey("UploadUserId")]
        public User? UploadUserFK { get; set; }

        public string? UploadReportFileName { get; set; }

        public bool? UploadSuccessFlag { get; set; }
    }
}
