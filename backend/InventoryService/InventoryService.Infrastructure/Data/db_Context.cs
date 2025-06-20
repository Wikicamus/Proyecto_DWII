using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using InventoryService.Domain.Models;

namespace InventoryService.Infrastructure.Data;

public partial class db_Context : DbContext
{
    public db_Context()
    {
    }

    public db_Context(DbContextOptions<db_Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventory> Inventories { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;database=gestion_ventas");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Inventory");

            entity.HasIndex(e => e.IdProduct, "fk_inventory_product");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.IdEmployee)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_employee");

            entity.Property(e => e.IdProduct)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_product");

            entity.Property(e => e.MovementDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("movement_date");

            entity.Property(e => e.MovementType)
                .HasDefaultValueSql("'IN'")
                .HasColumnType("enum('IN','OUT')")
                .HasColumnName("movement_type");

            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("1")
                .HasColumnType("int(11)")
                .HasColumnName("quantity");

            entity.HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inventory_product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.HasIndex(e => e.IdSupplier, "id_supplier");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");

            entity.Property(e => e.IdSupplier)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_supplier");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.Price)
                .HasColumnName("price");

            entity.Property(e => e.Stock)
                .HasColumnType("int(11)")
                .HasColumnName("stock");

            entity.HasOne(d => d.Supplier)
                .WithMany()
                .HasForeignKey(d => d.IdSupplier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_supplier");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Supplier");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.Phone)
                .HasColumnType("int(11)")
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
