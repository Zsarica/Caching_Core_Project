﻿// <auto-generated />
using System;
using CustomSqlServerCache.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomSqlServerCache.Migrations
{
    [DbContext(typeof(CacheDbContext))]
    [Migration("20250106081525_new_initial3")]
    partial class new_initial3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomSqlServerCache.Domain.Entities.CacheEntity", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Key");

                    b.ToTable("Caches", (string)null);
                });

            modelBuilder.Entity("CustomSqlServerCache.Domain.Entities.CacheEntity", b =>
                {
                    b.OwnsOne("Cache.Common.CacheItem", "CacheItem", b1 =>
                        {
                            b1.Property<string>("CacheEntityKey")
                                .HasColumnType("nvarchar(1000)");

                            b1.Property<DateTime?>("ExpiryDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Key")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CacheEntityKey");

                            b1.ToTable("Caches");

                            b1.ToJson("CacheItem");

                            b1.WithOwner()
                                .HasForeignKey("CacheEntityKey");
                        });

                    b.Navigation("CacheItem")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
