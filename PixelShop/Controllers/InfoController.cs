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
        public ActionResult Index(string id)
        {
            PixelShopEntities db = new PixelShopEntities();
            SANPHAM sp = (SANPHAM)db.SANPHAMs.Single(x => x.MaSP == id);
            sp.SoLuotXem++;
            db.SaveChanges();
            ViewData["hinhanh"] = db.HINHANHs.Where(x => x.MaSP == id).ToList<HINHANH>();
            ViewData["sanphamtuongtu"] = db.SANPHAMs.Where(x => x.DanhMuc == sp.DanhMuc && x.NhaSanXuat == sp.NhaSanXuat).ToList<SANPHAM>();
            return View(sp);
        }
    }
}