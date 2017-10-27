﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using CS586MVC.Models;

namespace CS586MVC.Data
{
    public partial class PropertyMismanagementContext : DbContext
    {
        public virtual DbSet<Complex> Complex { get; set; }
        public virtual DbSet<ComplexUnit> ComplexUnit { get; set; }
        public virtual DbSet<Lease> Lease { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }

        public PropertyMismanagementContext(DbContextOptions<PropertyMismanagementContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(@"Server=localhost;Database=PropertyMismanagement;User Id=sa;Password=PlzOpen4Me;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Complex>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Complex_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ComplexUnit>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("ComplexUnit_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ComplexId).HasColumnName("ComplexID");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.UnitNumber).HasColumnName("UnitNumber");
                
                entity.HasOne(d => d.Complex)
                    .WithMany(p => p.ComplexUnit)
                    .HasForeignKey(d => d.ComplexId)
                    .HasConstraintName("ComplexUnit_Complex_ID_fk");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.ComplexUnit)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("ComplexUnit_Unit_ID_fk");
            });

            modelBuilder.Entity<Lease>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Lease_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ComplexUnitId).HasColumnName("ComplexUnitID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.ComplexUnit)
                    .WithMany(p => p.Lease)
                    .HasForeignKey(d => d.ComplexUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Lease_ComplexUnit_ID_fk");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Lease)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Lease_Person_ID_fk");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Person_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });
        }
    }
}