using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class Hoadon
    {
        public Hoadon()
        {
            Cthoadons = new HashSet<Cthoadon>();
        }

        public int Mahd { get; set; }
        public DateTime Ngaylap { get; set; }
        public int IdKhachhang { get; set; }
        public int IdNhanvien { get; set; }
        public decimal Trigia { get; set; }

        public virtual Khachhang IdKhachhangNavigation { get; set; } = null!;
        public virtual Nhanvien IdNhanvienNavigation { get; set; } = null!;
        public virtual ICollection<Cthoadon> Cthoadons { get; set; }
    }
}
