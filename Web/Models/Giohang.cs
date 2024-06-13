using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class Giohang
    {
        public int IdGiohang { get; set; }
        public int IdKhachhang { get; set; }
        public string Masp { get; set; } = null!;

        public virtual Khachhang IdKhachhangNavigation { get; set; } = null!;
        public virtual Sanpham MaspNavigation { get; set; } = null!;
    }
}
