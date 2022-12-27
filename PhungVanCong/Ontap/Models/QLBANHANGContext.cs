using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Ontap.Models
{
    public partial class QLBANHANGContext : DbContext
    {
        public QLBANHANGContext()
        {
        }

        public QLBANHANGContext(DbContextOptions<QLBANHANGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-MRR2QS9\\SQLEXPRESS;Initial Catalog=QLBANHANG;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<LoaiSanPham>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiSanP__730A5759414CC295");

                entity.ToTable("LoaiSanPham");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.TenLoai).HasMaxLength(50);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__SanPham__2725081CEE1499ED");

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .HasColumnName("MaSP")
                    .IsFixedLength(true);

                entity.Property(e => e.MaLoai)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.TenSp)
                    .HasMaxLength(50)
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("fk_LoaiSanPham_SanPham");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
