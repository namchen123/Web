using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Numerics;
using Web.Models;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShopDienThoaiContext _context;
        private List<Sanpham> sanphams = new List<Sanpham>();
        public OrderController(ShopDienThoaiContext context)
        {
            _context = context;
        }

        public IActionResult ProductDetail(String id,String loaisp)
        {           
            ViewBag.Cart = HttpContext.Session.GetInt32("Count");
            var detail=_context.Sanphams.Where(p => p.Masp.Equals(id)).ToList();
            var relatedProduct = _context.Sanphams.Where(p => p.Loaisp.Equals(loaisp)).ToList();
            ViewBag.RelatedProduct = relatedProduct;
            return View(detail);
        }
        [HttpPost]
        public IActionResult AddToCart(String masp)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng.";
                return RedirectToAction("Shop", "HomePage");
            }
            var sanpham=_context.Sanphams.SingleOrDefault(p=>p.Masp.Equals(masp));
            var nguoidung = _context.Nguoidungs.SingleOrDefault(p => p.Tendangnhap.Equals(User.Identity.Name));
            var khachhang = _context.Khachhangs.SingleOrDefault(p => p.IdNguoidung.Equals(nguoidung.IdNguoidung));
            Giohang giohang = new Giohang();
            giohang.IdKhachhang = khachhang.IdKhachhang;
            giohang.Masp = sanpham.Masp;
            _context.Giohangs.Add(giohang);
            _context.SaveChanges();
            return RedirectToAction("Shop","HomePage");
        }
        public IActionResult Cart()
        {

            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "Vui lòng đăng nhập để xem giỏ hàng";
                return RedirectToAction("Shop", "HomePage");
            }
            var nguoidung = _context.Nguoidungs.SingleOrDefault(p => p.Tendangnhap.Equals(User.Identity.Name));
            var khachhang = _context.Khachhangs.SingleOrDefault(p => p.IdNguoidung.Equals(nguoidung.IdNguoidung));
            ViewBag.MaKH = khachhang.IdKhachhang;
            var giohang = _context.Giohangs.Where(p => p.IdKhachhang.Equals(khachhang.IdKhachhang)).ToList();
            var sanpham = new List<Sanpham>();
            List<GH> giohangmodel = new List<GH>();
            foreach (var item in giohang)
            {
                GH gh = new GH();
                gh.IdGiohang = item.IdGiohang;
                var sp = _context.Sanphams.SingleOrDefault(p => p.Masp.Equals(item.Masp));
                gh.Masp = sp.Masp;
                gh.Tensp = sp.Tensp;
                gh.Loaisp = sp.Loaisp;
                gh.Mota = sp.Mota;
                gh.Hangsx = sp.Hangsx;
                gh.Giaban = sp.Giaban;
                gh.Soluong = sp.Soluong;
                gh.Hinhanh = sp.Hinhanh;
                if (gh != null)
                {
                    giohangmodel.Add(gh);
                }
            }
            return View(giohangmodel);
        }
        public IActionResult DeleteCart(String masp,String makh, int magiohang)
        {
            var giohang = _context.Giohangs.Where(g => g.IdKhachhang == Int32.Parse(makh) && g.Masp == masp && g.IdGiohang== magiohang).FirstOrDefault();
            _context.Giohangs.Remove(giohang);
            _context.SaveChanges();
            return RedirectToAction("Cart");
        }
        [HttpPost]
        public IActionResult OrderSuccess(List<GH> items, int makhachhang)
        {
            var hoadon = new Hoadon();
            hoadon.IdKhachhang = makhachhang;
            hoadon.Ngaylap = DateTime.Now;
            ViewBag.Ngaylap= DateTime.Now;
            hoadon.IdNhanvien = 1;
            hoadon.Trigia = items.Sum(p => p.Giaban);
            ViewBag.Total = hoadon.Trigia;
            ViewBag.Mahoadon = hoadon.Mahd;
            _context.Add(hoadon);
            _context.SaveChanges();
            foreach (var product in items)
            {
                var chitiethoadon = new Cthoadon();
                chitiethoadon.Mahd = hoadon.Mahd;
                chitiethoadon.Soluong = 1;
                chitiethoadon.Dongbia = product.Giaban;
                chitiethoadon.Masp = product.Masp;
                
                var giohang = _context.Giohangs.Where(p => p.IdKhachhang == makhachhang).FirstOrDefault();
                var sanpham = _context.Sanphams.First(p => p.Masp == chitiethoadon.Masp);
                sanpham.Soluong--;
                _context.Remove(giohang);
                _context.Add(chitiethoadon);
                _context.SaveChanges();
            }
            return View(items);
        }
        public IActionResult OrderHistory()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "Vui lòng đăng nhập để xem lịch sử đặt hàng";
                return RedirectToAction("Shop", "HomePage");
            }
            var nguoidung = _context.Nguoidungs.SingleOrDefault(p => p.Tendangnhap.Equals(User.Identity.Name));
            var khachhang = _context.Khachhangs.SingleOrDefault(p=>p.IdNguoidung.Equals(nguoidung.IdNguoidung));
            var hoadon = _context.Hoadons.Where(p => p.IdKhachhang.Equals(khachhang.IdKhachhang));
            var cthoadon = _context.Cthoadons.Include(p=>p.MaspNavigation).Include(p=>p.MahdNavigation).Where(p=>p.MahdNavigation.IdKhachhang.Equals(khachhang.IdKhachhang)).ToList();
            ViewBag.hoadon = hoadon;
            return View(cthoadon);
        }
    }
}
