using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class Nguoidung
    {
        public Nguoidung()
        {
            Khachhangs = new HashSet<Khachhang>();
            Nhanviens = new HashSet<Nhanvien>();
        }

        public int IdNguoidung { get; set; }
        public string Tendangnhap { get; set; } = null!;
        public string Matkhau { get; set; } = null!;
        public string Vaitro { get; set; } = null!;

        public virtual ICollection<Khachhang> Khachhangs { get; set; }
        public virtual ICollection<Nhanvien> Nhanviens { get; set; }
    }
}
