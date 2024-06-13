using Microsoft.AspNetCore.Mvc;
using ShopBanHangOnline.Data;
using ShopBanHangOnline.ViewModels;

namespace ShopBanHangOnline.Viewcomponents
{
    public class MenuLoaiViewComponent: ViewComponent
    {
        private readonly ShopBanHangContext db;

        public MenuLoaiViewComponent(ShopBanHangContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new MenuLoaiVM
            {
                MaLoai= lo.MaLoai, 
                TenLoai= lo.TenLoai,
                SoLuong= lo.HangHoas.Count
            }).OrderBy(p=>p.TenLoai);
            return View(data);
        }
    }
}
