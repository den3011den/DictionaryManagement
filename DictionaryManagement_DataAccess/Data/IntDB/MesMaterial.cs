﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("MesMaterial", Schema = "dbo")]
    public class MesMaterial
    {
        [Key]
        [MaxLength(100)]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string ShortName { get; set; } = string.Empty;

        public bool IsArchive { get; set; }
    }
}
