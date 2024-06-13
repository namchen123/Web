using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
	public class HomePageController : Controller
	{
		private readonly ShopDienThoaiContext _context;
		public HomePageController(ShopDienThoaiContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
            var nguoidung = _context.Nguoidungs.SingleOrDefault(p => p.Tendangnhap.Equals(User.Identity.Name));

            if (_context.Nhanviens.SingleOrDefault(p => p.IdNguoidung.Equals(nguoidung.IdNguoidung)) != null)
            {
                return View();
            }
            if (User.Identity.IsAuthenticated)
			{
                var khachhang = _context.Khachhangs.SingleOrDefault(p => p.IdNguoidung.Equals(nguoidung.IdNguoidung));
                ViewBag.Cart = _context.Giohangs.Count(p => p.IdKhachhang.Equals(khachhang.IdKhachhang));
            }
			return View();
		}
		public IActionResult About()
		{

            var nguoidung = _context.Nguoidungs.SingleOrDefault(p => p.Tendangnhap.Equals(User.Identity.Name));

            if (_context.Nhanviens.SingleOrDefault(p => p.IdNguoidung.Equals(nguoidung.IdNguoidung)) != null)
            {
                return View();
            }
            if (User.Identity.IsAuthenticated)
            {
                var khachhang = _context.Khachhangs.SingleOrDefault(p => p.IdNguoidung.Equals(nguoidung.IdNguoidung));
                ViewBag.Cart = _context.Giohangs.Count(p => p.IdKhachhang.Equals(khachhang.IdKhachhang));
            }
            return View();
		}
		public IActionResult Shop()
		{
			ViewBag.ListBrand = _context.Sanphams.Select(p=>p.Hangsx).Distinct().ToList();
			if (!User.Identity.IsAuthenticated)
			{
                ViewBag.mess = TempData["Message"];
                var shop1 = _context.Sanphams.ToList();
                return View(shop1);
            }
			
            var nguoidung = _context.Nguoidungs.SingleOrDefault(p => p.Tendangnhap.Equals(User.Identity.Name));
			if (_context.Nhanviens.SingleOrDefault(p => p.IdNguoidung.Equals(nguoidung.IdNguoidung)) != null)
			{
                var shop2 = _context.Sanphams.ToList();
                return View(shop2);
            }
            var khachhang = _context.Khachhangs.SingleOrDefault(p => p.IdNguoidung.Equals(nguoidung.IdNguoidung));
			ViewBag.Cart = _context.Giohangs.Count(p => p.IdKhachhang.Equals(khachhang.IdKhachhang));
            var shop = _context.Sanphams.ToList();
            return View(shop);
        }
		public IActionResult ShopByModel(String? name)
		{
            var sanpham = _context.Sanphams.Where(p=>p.Loaisp.Equals(name)).ToList();
			return PartialView("ShopByModel",sanpham);
		}
		public IActionResult ShopByBrand(String? brand)
		{
			var sanpham = _context.Sanphams.Where(p=>p.Hangsx.Equals(brand)).ToList();
			return PartialView("ShopByModel", sanpham);
		}
        public IActionResult ShopByName(String? name)
        {
            var sanpham = _context.Sanphams.Where(p => p.Tensp.Contains(name)).ToList();
            return PartialView("ShopByModel", sanpham);
        }
    }
}
