using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopDienThoaiContext _context;
        public AccountController(ShopDienThoaiContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(String Username,String Password)
        {
           
                var user = _context.Nguoidungs.FirstOrDefault(p => p.Tendangnhap == Username && p.Matkhau == Password);

                if (user != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username),
                    new Claim(ClaimTypes.Role, user.Vaitro)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {

                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return RedirectToAction("Index", "HomePage");
                }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Nguoidung nguoidung)
        {
            if(ModelState.IsValid)
            {
                if (_context.Nguoidungs.SingleOrDefault(p => p.Tendangnhap == nguoidung.Tendangnhap) == null)
                {
                    _context.Nguoidungs.Add(nguoidung);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                ViewBag.Error = "Tài khoản đã tồn tại";
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "HomePage");
        }
        public IActionResult GuestDetail(String user)
        {
            var guestdetail = _context.Khachhangs.SingleOrDefault(p=>p.IdNguoidungNavigation.Tendangnhap.Equals(user));
            var User = _context.Nguoidungs.SingleOrDefault(p => p.Tendangnhap.Equals(user));
            ViewBag.Id = User.IdNguoidung;
            return View(guestdetail);
        }
        [HttpPost]
        public IActionResult GuestDetail(Khachhang khachhang)
        {

            if (!ModelState.IsValid)
            {
                var existingKhachhang = _context.Khachhangs.AsNoTracking().SingleOrDefault(p => p.IdNguoidung.Equals(khachhang.IdNguoidung));

                if (existingKhachhang == null)
                {
                    _context.Khachhangs.Add(khachhang);
                    ViewBag.Message = "Thêm mới khách hàng thành công!";
                }
                else
                {
                    _context.Khachhangs.Update(khachhang);
                    ViewBag.Message = "Cập nhật thông tin khách hàng thành công!";
                }

                _context.SaveChanges();
                return RedirectToAction("Index", "HomePage");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                System.Console.WriteLine(error.ErrorMessage);
            }
            return View();
        }
    }
}
