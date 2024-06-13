using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Web.Models
{
    public partial class ShopDienThoaiContext : DbContext
    {
        public ShopDienThoaiContext()
        {
        }

        public ShopDienThoaiContext(DbContextOptions<ShopDienThoaiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cthoadon> Cthoadons { get; set; } = null!;
        public virtual DbSet<Giohang> Giohangs { get; set; } = null!;
        public virtual DbSet<Hoadon> Hoadons { get; set; } = null!;
        public virtual DbSet<Khachhang> Khachhangs { get; set; } = null!;
        public virtual DbSet<Nguoidung> Nguoidungs { get; set; } = null!;
        public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; } = null!;
        public virtual DbSet<Nhanvien> Nhanviens { get; set; } = null!;
        public virtual DbSet<Sanpham> Sanphams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-V23QTQA;Initial Catalog=ShopDienThoai;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cthoadon>(entity =>
            {
                entity.HasKey(e => e.Cthdid)
                    .HasName("PK__CTHOADON__C41CF8E8E7751EB3");

                entity.ToTable("CTHOADON");

                entity.Property(e => e.Cthdid).HasColumnName("CTHDID");

                entity.Property(e => e.Dongbia)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("DONGBIA");

                entity.Property(e => e.Mahd).HasColumnName("MAHD");

                entity.Property(e => e.Masp)
                    .HasMaxLength(20)
                    .HasColumnName("MASP");

                entity.Property(e => e.Soluong).HasColumnName("SOLUONG");

                entity.HasOne(d => d.MahdNavigation)
                    .WithMany(p => p.Cthoadons)
                    .HasForeignKey(d => d.Mahd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTHOADON_HOADON");

                entity.HasOne(d => d.MaspNavigation)
                    .WithMany(p => p.Cthoadons)
                    .HasForeignKey(d => d.Masp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTHOADON_SANPHAM");
            });

            modelBuilder.Entity<Giohang>(entity =>
            {
                entity.HasKey(e => e.IdGiohang)
                    .HasName("PK__GIOHANG__4A3451243FDE258B");

                entity.ToTable("GIOHANG");

                entity.Property(e => e.IdGiohang).HasColumnName("ID_GIOHANG");

                entity.Property(e => e.IdKhachhang).HasColumnName("ID_KHACHHANG");

                entity.Property(e => e.Masp)
                    .HasMaxLength(20)
                    .HasColumnName("MASP");

                entity.HasOne(d => d.IdKhachhangNavigation)
                    .WithMany(p => p.Giohangs)
                    .HasForeignKey(d => d.IdKhachhang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GIOHANG_KHACHHANG");

                entity.HasOne(d => d.MaspNavigation)
                    .WithMany(p => p.Giohangs)
                    .HasForeignKey(d => d.Masp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GIOHANG_SANPHAM");
            });

            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasKey(e => e.Mahd)
                    .HasName("PK__HOADON__603F20CEA5EFF768");

                entity.ToTable("HOADON");

                entity.Property(e => e.Mahd).HasColumnName("MAHD");

                entity.Property(e => e.IdKhachhang).HasColumnName("ID_KHACHHANG");

                entity.Property(e => e.IdNhanvien).HasColumnName("ID_NHANVIEN");

                entity.Property(e => e.Ngaylap)
                    .HasColumnType("date")
                    .HasColumnName("NGAYLAP");

                entity.Property(e => e.Trigia)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("TRIGIA");

                entity.HasOne(d => d.IdKhachhangNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.IdKhachhang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOADON_KHACHHANG");

                entity.HasOne(d => d.IdNhanvienNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.IdNhanvien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOADON_NHANVIEN");
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.IdKhachhang)
                    .HasName("PK__KHACHHAN__769C0DEBA0DE9506");

                entity.ToTable("KHACHHANG");

                entity.Property(e => e.IdKhachhang).HasColumnName("ID_KHACHHANG");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(255)
                    .HasColumnName("DIACHI");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(20)
                    .HasColumnName("DIENTHOAI");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(50)
                    .HasColumnName("HOTEN");

                entity.Property(e => e.IdNguoidung).HasColumnName("ID_NGUOIDUNG");

                entity.HasOne(d => d.IdNguoidungNavigation)
                    .WithMany(p => p.Khachhangs)
                    .HasForeignKey(d => d.IdNguoidung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KHACHHANG_NGUOIDUNG");
            });

            modelBuilder.Entity<Nguoidung>(entity =>
            {
                entity.HasKey(e => e.IdNguoidung)
                    .HasName("PK__NGUOIDUN__63AAF5E10DB54361");

                entity.ToTable("NGUOIDUNG");

                entity.HasIndex(e => e.Tendangnhap, "UQ__NGUOIDUN__6C836FE5394BF43E")
                    .IsUnique();

                entity.Property(e => e.IdNguoidung).HasColumnName("ID_NGUOIDUNG");

                entity.Property(e => e.Matkhau)
                    .HasMaxLength(255)
                    .HasColumnName("MATKHAU");

                entity.Property(e => e.Tendangnhap)
                    .HasMaxLength(50)
                    .HasColumnName("TENDANGNHAP");

                entity.Property(e => e.Vaitro)
                    .HasMaxLength(20)
                    .HasColumnName("VAITRO");
            });

            modelBuilder.Entity<Nhacungcap>(entity =>
            {
                entity.HasKey(e => e.Mancc)
                    .HasName("PK__NHACUNGC__7ABEA5821F512C48");

                entity.ToTable("NHACUNGCAP");

                entity.Property(e => e.Mancc)
                    .HasMaxLength(20)
                    .HasColumnName("MANCC");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(255)
                    .HasColumnName("DIACHI");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(20)
                    .HasColumnName("DIENTHOAI");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Tenncc)
                    .HasMaxLength(50)
                    .HasColumnName("TENNCC");
            });

            modelBuilder.Entity<Nhanvien>(entity =>
            {
                entity.HasKey(e => e.IdNhanvien)
                    .HasName("PK__NHANVIEN__DE90FCA31CB7B22F");

                entity.ToTable("NHANVIEN");

                entity.Property(e => e.IdNhanvien).HasColumnName("ID_NHANVIEN");

                entity.Property(e => e.Chucvu)
                    .HasMaxLength(30)
                    .HasColumnName("CHUCVU");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(50)
                    .HasColumnName("HOTEN");

                entity.Property(e => e.IdNguoidung).HasColumnName("ID_NGUOIDUNG");

                entity.Property(e => e.Phongban)
                    .HasMaxLength(30)
                    .HasColumnName("PHONGBAN");

                entity.HasOne(d => d.IdNguoidungNavigation)
                    .WithMany(p => p.Nhanviens)
                    .HasForeignKey(d => d.IdNguoidung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NHANVIEN_NGUOIDUNG");
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.HasKey(e => e.Masp)
                    .HasName("PK__SANPHAM__60228A3276D41D5F");

                entity.ToTable("SANPHAM");

                entity.Property(e => e.Masp)
                    .HasMaxLength(20)
                    .HasColumnName("MASP");

                entity.Property(e => e.Giaban)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("GIABAN");

                entity.Property(e => e.Hangsx)
                    .HasMaxLength(30)
                    .HasColumnName("HANGSX");

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(255)
                    .HasColumnName("HINHANH");

                entity.Property(e => e.Loaisp)
                    .HasMaxLength(30)
                    .HasColumnName("LOAISP");

                entity.Property(e => e.Mota)
                    .HasMaxLength(1000)
                    .HasColumnName("MOTA");

                entity.Property(e => e.Soluong).HasColumnName("SOLUONG");

                entity.Property(e => e.Tensp)
                    .HasMaxLength(50)
                    .HasColumnName("TENSP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
