using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Controllers
{
    public class ManagementController : Controller
    {
        private readonly ShopDienThoaiContext _context;
        public ManagementController(ShopDienThoaiContext context)
        {
            _context = context;
        }

        public IActionResult SanPhamManagement()
        {
            var sanpham = _context.Sanphams.ToList();
            return View(sanpham);
        }
        public IActionResult KhachHangManagement()
        {
            var khachhang = _context.Khachhangs.Include(p=>p.IdNguoidungNavigation).ToList();
            return View(khachhang);
        }
        public IActionResult CreateSanPham()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSanPham(Sanpham sanpham)
        {
            _context.Sanphams.Add(sanpham);
            _context.SaveChanges();
            return RedirectToAction("SanPhamManagement");
        }
        public IActionResult EditSanPham(String masp)
        {
            var sanpham = _context.Sanphams.SingleOrDefault(p=>p.Masp.Equals(masp));
            return View(sanpham);
        }
        [HttpPost]
        public IActionResult EditSanPham(Sanpham sanpham)
        {
            _context.Sanphams.Update(sanpham);
            _context.SaveChanges();
            return RedirectToAction("SanPhamManagement");
        }
        public IActionResult DeleteSanPham(String masp)
        {
            var sanpham = _context.Sanphams.SingleOrDefault(p=>p.Masp.Equals(masp));
            _context.Sanphams.Remove(sanpham);
            _context.SaveChanges();
            return RedirectToAction("SanPhamManagement");
        }
        public IActionResult EditKhachHang(int makhachhang)
        {
            var khachhang = _context.Khachhangs.Where(p=>p.IdKhachhang == makhachhang).Include(p=>p.IdNguoidungNavigation).SingleOrDefault();
            
            return View(khachhang);
        }
        [HttpPost]
        public async Task<IActionResult> EditKhachHang(Khachhang khachhang, int id)
        {
            var khachHang = await _context.Khachhangs.Include(k => k.IdNguoidungNavigation).FirstOrDefaultAsync(k => k.IdKhachhang == id);
            khachHang.Hoten = khachhang.Hoten;
            khachHang.Diachi = khachhang.Diachi;
            khachHang.Dienthoai = khachhang.Dienthoai;
            khachHang.IdNguoidungNavigation.Tendangnhap = khachhang.IdNguoidungNavigation.Tendangnhap;
            khachHang.IdNguoidungNavigation.Matkhau = khachhang.IdNguoidungNavigation.Matkhau;

            // Lưu thay đổi
            _context.Update(khachHang);
            await _context.SaveChangesAsync();
            return RedirectToAction("KhachHangManagement");
        }
        public IActionResult DeleteKhachHang(int makhachhang)
        {
            var khachhang = _context.Khachhangs.SingleOrDefault(p=>p.IdKhachhang == makhachhang);
            var nguoidung = _context.Nguoidungs.SingleOrDefault(p => p.IdNguoidung == khachhang.IdNguoidung);
            _context.Khachhangs.Remove(khachhang);
            _context.SaveChanges();
            _context.Nguoidungs.Remove(nguoidung);
            _context.SaveChanges();
            return RedirectToAction("KhachHangManagement");
        }
       
    }
}
