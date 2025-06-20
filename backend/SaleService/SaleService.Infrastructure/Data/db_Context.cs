using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SaleService.Domain.Models;

namespace SaleService.Infrastructure.Data;

public partial class db_Context : DbContext
{
    public db_Context()
    {
    }

    public db_Context(DbContextOptions<db_Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<Complaint> Complaints { get; set; }
    public virtual DbSet<DeliveryStatus> DeliveryStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;database=gestion_ventas");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("Sale");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IdClient)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_client");
            entity.Property(e => e.IdProduct)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_product");
            entity.Property(e => e.Units)
                .HasColumnType("int(11)")
                .HasColumnName("Units");
            entity.Property(e => e.Total)
                .HasColumnType("double")
                .HasColumnName("total");
            entity.HasMany(d => d.DeliveryStatuses)
                .WithOne(p => p.IdSaleNavigation)
                .HasForeignKey(d => d.IdSale)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("DeliveryStatus_ibfk_1");
        });

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("Complaint");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.IdSale)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("idsale");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("reason");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description");

            entity.HasOne(d => d.IdSaleNavigation)
                .WithMany()
                .HasForeignKey(d => d.IdSale)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Complaint_ibfk_1");
        });

        modelBuilder.Entity<DeliveryStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("DeliveryStatus");

            entity.HasIndex(e => e.IdSale, "idsale");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.IdSale)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("idsale");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("state");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description");

            entity.HasOne(d => d.IdSaleNavigation)
                .WithMany(p => p.DeliveryStatuses)
                .HasForeignKey(d => d.IdSale)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("DeliveryStatus_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
