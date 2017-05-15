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
            ViewData["hinhanh"] = db.HINHANHs.Where(x => x.MaSP == id).ToList<HINHANH>();
            return View(sp);
        }
    }
}