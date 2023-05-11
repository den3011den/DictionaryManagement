﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

        public DbSet<MesParamSourceType> MesParamSourceType { get; set; }
        public DbSet<DataType> DataType { get; set; }
        public DbSet<DataSource> DataSource { get; set; }
        public DbSet<ReportTemplateType> ReportTemplateType { get; set; }
        public DbSet<LogEventType> LogEventType { get; set; }
        public DbSet<Settings> Settings { get; set; }

        public DbSet<UnitOfMeasureSapToMesMapping> UnitOfMeasureSapToMesMapping { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<UnitOfMeasureSapToMesMapping>().HasKey(x => new { x.SapUnitId, x.MesUnitId });
        //}

    }

}
