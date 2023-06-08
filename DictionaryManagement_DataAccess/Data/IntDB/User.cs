﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    [Table("User", Schema = "dbo")]
    public class User
    {

        [Key]        
        [Required]
        public string Id { get; set; }

        [Required]
        public string Login { get; set; }        

        public string? UserName { get; set; }

        public string? Description { get; set; }

        public bool IsArchive { get; set; }
    }

}
