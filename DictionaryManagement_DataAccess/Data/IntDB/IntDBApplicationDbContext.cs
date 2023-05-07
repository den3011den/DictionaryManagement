using Microsoft.EntityFrameworkCore;
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
        public DbSet<MesUnitOfMeasure> MesMaterial { get; set; }
        public DbSet<MesUnitOfMeasure> SapMaterial { get; set; }

    }

}
