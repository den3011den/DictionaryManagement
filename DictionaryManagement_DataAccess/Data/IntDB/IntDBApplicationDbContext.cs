﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_DataAccess.Data.IntDB
{
    public class IntDBApplicationDbContext : DbContext
    {
        public IntDBApplicationDbContext(DbContextOptions<IntDBApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<SapEquipment> SapEquipment { get; set; }
        public DbSet<SapUnitOfMeasure> SapUnitOfMeasure { get; set; }
        public DbSet<MesUnitOfMeasure> MesUnitOfMeasure { get; set; }
        public DbSet<MesMaterial> MesMaterial { get; set; }
        public DbSet<SapMaterial> SapMaterial { get; set; }
        public DbSet<ErrorCriterion> ErrorCriterion { get; set; }
        public DbSet<CorrectionReason> CorrectionReason { get; set; }

    }

}
