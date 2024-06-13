using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class Cthoadon
    {
        public int Cthdid { get; set; }
        public int Mahd { get; set; }
        public string Masp { get; set; } = null!;
        public int Soluong { get; set; }
        public decimal Dongbia { get; set; }

        public virtual Hoadon MahdNavigation { get; set; } = null!;
        public virtual Sanpham MaspNavigation { get; set; } = null!;
    }
}
