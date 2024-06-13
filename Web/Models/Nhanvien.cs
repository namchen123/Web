using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class Nhanvien
    {
        public Nhanvien()
        {
            Hoadons = new HashSet<Hoadon>();
        }

        public int IdNhanvien { get; set; }
        public string Hoten { get; set; } = null!;
        public string? Chucvu { get; set; }
        public string? Phongban { get; set; }
        public int IdNguoidung { get; set; }

        public virtual Nguoidung IdNguoidungNavigation { get; set; } = null!;
        public virtual ICollection<Hoadon> Hoadons { get; set; }
    }
}
