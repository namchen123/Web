namespace Web.Models
{
    public class GH
    {
        public int IdGiohang { get; set; }
        public string Masp { get; set; } = null!;
        public string Tensp { get; set; } = null!;
        public string? Mota { get; set; }
        public string Loaisp { get; set; } = null!;
        public string? Hangsx { get; set; }
        public decimal Giaban { get; set; }
        public int Soluong { get; set; }
        public string? Hinhanh { get; set; }

    }
}
