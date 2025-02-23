﻿using Microsoft.AspNetCore.Mvc;
using ShopBanHangOnline.Helpers;
using ShopBanHangOnline.ViewModels;

namespace ShopBanHangOnline.Viewcomponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
            return View("CartPanel", new CartModels
            {
                Quantity = cart.Sum(p => p.SoLuong),
                Total = cart.Sum(p => p.ThanhTien)
            });
        }
    }
}
