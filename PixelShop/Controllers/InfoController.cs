using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class InfoController : Controller
    {
        // GET: Info
        public ActionResult Index(string code)
        {
            PixelShopEntities db = new PixelShopEntities();
            SANPHAM sp = (SANPHAM)db.SANPHAMs.SingleOrDefault(x => x.MaSP == code);
            sp.SoLuotXem++;
            db.SaveChanges();
            ViewData["hinhanh"] = db.HINHANHs.Where(x => x.MaSP == code).ToList<HINHANH>();
            ViewData["sanphamtuongtu"] = db.SANPHAMs.Where(x => x.DanhMuc == sp.DanhMuc && x.NhaSanXuat == sp.NhaSanXuat && x.MaSP != code).ToList<SANPHAM>();
            return View(sp);
        }
    }
}