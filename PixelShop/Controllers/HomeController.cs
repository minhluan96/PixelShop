using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PixelShop.Models;


namespace PixelShop.Controllers
{
    public class HomeController : Controller
    {
        PixelShopEntities db = new PixelShopEntities();

        public ActionResult Index()
        {
            List<SANPHAM> listTop8SanPham = db.SANPHAMs.Where(sp => sp.BiXoa == 0 && sp.SoLuongTon>0).Select(sp => sp).Take(8).ToList<SANPHAM>();
            List<SANPHAM> lstSanPhamXemNhieu = db.SANPHAMs.Where(sp => sp.BiXoa == 0 && sp.SoLuongTon > 0).OrderBy(sp => sp.SoLuotXem).Take(5).ToList<SANPHAM>();

            ViewData["banChay"] = listTop8SanPham;
            ViewData["xemNhieu"] = lstSanPhamXemNhieu;

            return View();
        }
    }
}