﻿using EasyProfiler.Core.Entities;
using EasyProfiler.Core.Helpers.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyProfiler.PostgreSQL.Context
{
    /// <summary>
    /// Profiler DbContext for PostgreSQL
    /// </summary>
    public class ProfilerDbContext : DbContext
    {
        public ProfilerDbContext(DbContextOptions<ProfilerDbContext> options) : base(options)
        {
        }

        protected ProfilerDbContext()
        {
        }

        #region Tables
        public virtual DbSet<Profiler> Profilers { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profiler>(entity =>
            {
                entity
                    .HasKey(pk => pk.Id);

                entity
                    .HasIndex(i => i.Duration);

                entity
                   .Property(p => p.Id)
                   .HasValueGenerator<GuidGenerator>()
                   .ValueGeneratedOnAdd();

                entity
                    .Property(p => p.Query)
                    .IsRequired();

                entity
                    .Property(p => p.QueryType)
                    .IsRequired()
                    .HasConversion(new EnumToStringConverter<QueryType>());

                entity
                    .Property(p => p.Duration)
                    .HasColumnType("bigint");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
