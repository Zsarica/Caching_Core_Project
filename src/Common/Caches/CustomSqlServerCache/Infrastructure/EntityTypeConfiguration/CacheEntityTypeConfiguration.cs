using CustomSqlServerCache.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomSqlServerCache.Infrastructure.EntityTypeConfiguration
{
    public class CacheEntityTypeConfiguration : IEntityTypeConfiguration<CacheEntity>
    {
        public void Configure(EntityTypeBuilder<CacheEntity> builder)
        {
            
            builder.ToTable(name: "Caches");
            builder.HasKey(i => i.Key);
            builder.Property(i => i.Key)
                .IsRequired(true)
                .HasMaxLength(1000);
            builder.OwnsOne(i => i.CacheItem, builder =>
            {
                builder.Property(i => i.Value).HasConversion<ObjectStringValueConverter>();
                builder.ToJson();
            });
        }
    }
}

public class ObjectStringValueConverter : ValueConverter<object, string>
{
    public ObjectStringValueConverter() : base(v => v.ToString(), v => v)
    {

    }
}
