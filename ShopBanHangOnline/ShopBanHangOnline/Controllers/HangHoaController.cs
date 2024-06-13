using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBanHangOnline.Data;
using ShopBanHangOnline.ViewModels;

namespace ShopBanHangOnline.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly ShopBanHangContext db;

        public HangHoaController(ShopBanHangContext context) 
        {
            db = context;
        }
        public IActionResult Index(int? Loai)
        {
            var  hangHoas = db.HangHoas.AsQueryable();
            if(Loai.HasValue)
            {
                hangHoas = hangHoas.Where(p => p.MaLoai == Loai.Value);
            }
            var result = hangHoas.Select(p => new HangHoaVM
            {
                Mah = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ??0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        public IActionResult Search(string? query)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (query != null)
            {
                hangHoas = hangHoas.Where(p => p.TenHh.Contains(query));
            }
            var result = hangHoas.Select(p => new HangHoaVM
            {
                Mah = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        public IActionResult Detail(int id)
        {
            var data = db.HangHoas.Include(p=> p.MaLoaiNavigation).
                SingleOrDefault(p=>p.MaHh == id);
            if(data == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }
            var result = new ChiTietHangHoaVM
            {
                Mah = data.MaHh,
                TenHH = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? string.Empty,
                Hinh = data.Hinh ?? string.Empty,
                MoTaNgan = data.MoTaDonVi ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10,
                DiemDanhGia = 5
            };
            return View(result);
        }
    }
}
