﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("SapToMesMaterialMapping", Schema = "dbo")]
    public class SapToMesMaterialMapping
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
    
        public int SapMaterialId { get; set; }

        [ForeignKey("SapMaterialId")]
        public SapMaterial? SapMaterial { get; set; }

        public int MesMaterialId { get; set; }

        [ForeignKey("MesMaterialId")]
        public MesMaterial? MesMaterial { get; set; } 
        
    }

}
