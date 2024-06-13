using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class Sanpham
    {
        public Sanpham()
        {
            Cthoadons = new HashSet<Cthoadon>();
            Giohangs = new HashSet<Giohang>();
        }

        public string Masp { get; set; } = null!;
        public string Tensp { get; set; } = null!;
        public string? Mota { get; set; }
        public string Loaisp { get; set; } = null!;
        public string? Hangsx { get; set; }
        public decimal Giaban { get; set; }
        public int Soluong { get; set; }
        public string? Hinhanh { get; set; }

        public virtual ICollection<Cthoadon> Cthoadons { get; set; }
        public virtual ICollection<Giohang> Giohangs { get; set; }
    }
}
