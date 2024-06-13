using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Giohangs = new HashSet<Giohang>();
            Hoadons = new HashSet<Hoadon>();
        }

        public int IdKhachhang { get; set; }
        public string Hoten { get; set; } = null!;
        public string? Diachi { get; set; }
        public string? Dienthoai { get; set; }
        public int IdNguoidung { get; set; }

        public virtual Nguoidung IdNguoidungNavigation { get; set; } = null!;
        public virtual ICollection<Giohang> Giohangs { get; set; }
        public virtual ICollection<Hoadon> Hoadons { get; set; }
    }
}
