using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace QL_NhaHang.Models;

public partial class QlNhaHangContext : DbContext
{

    public QlNhaHangContext()
    {
    }
    public QlNhaHangContext(DbContextOptions<QlNhaHangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<DinnerTable> DinnerTables { get; set; }

    public virtual DbSet<ItemOrder> ItemOrders { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-LQMAAHM3\\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True;TrustServerCertificate=True");
    public static class CustomConverters
    {
        public static ValueConverter<TimeOnly, TimeSpan> TimeOnlyConverter => new ValueConverter<TimeOnly, TimeSpan>(
            v => v.ToTimeSpan(),
            v => TimeOnly.FromTimeSpan(v));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id); 

            entity.ToTable("Admin");

            entity.Property(e => e.Passwork)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.UserName)
                .HasMaxLength(50);

            entity.Property(e => e.Email)
                .HasMaxLength(100) 
                .IsUnicode(false); 
        });
        modelBuilder.Entity<DinnerTable>(entity =>
        {
            entity.ToTable("DinnerTable");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.client);
        });

        modelBuilder.Entity<ItemOrder>(entity =>
        {
            entity.ToTable("ItemOrder");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.ItemOrders)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK_ItemOrder_Menu");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.ItemOrders)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK_ItemOrder_Order");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Img).IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.NumberPhone)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Time)
                .HasConversion(CustomConverters.TimeOnlyConverter)
                .HasColumnType("time"); // Use value converter for TimeOnly

            entity.HasOne(d => d.IdTableNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdTable)
                .HasConstraintName("FK_Order_DinnerTable");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
