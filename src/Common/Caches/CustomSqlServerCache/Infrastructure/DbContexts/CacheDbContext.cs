using CustomSqlServerCache.Domain.Entities;
using CustomSqlServerCache.Infrastructure.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSqlServerCache.Infrastructure.DbContexts
{
    public class CacheDbContext(DbContextOptions options):DbContext(options)
    {
        public DbSet<CacheEntity> Cache { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CacheEntityTypeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
