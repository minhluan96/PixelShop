using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PixelShop.Models;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class MenuComponentController : Controller
    {
        PixelShopEntities db = new PixelShopEntities();
        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            List<NHASANXUAT> lstNSX = db.NHASANXUATs.Where(nsx => nsx.BiXoa == 0).Select(nsx => nsx).ToList<NHASANXUAT>();
            List<DANHMUC> lstDMNam = db.DANHMUCs.Where(dm => dm.BiXoa == 0 && dm.NhomDanhMuc== 1).Select(dm => dm).ToList<DANHMUC>();
            List<DANHMUC> lstDMNu = db.DANHMUCs.Where(dm => dm.BiXoa == 0 && dm.NhomDanhMuc == 2).Select(dm => dm).ToList<DANHMUC>();
            List<DANHMUC> lstDMPhuKien = db.DANHMUCs.Where(dm => dm.BiXoa == 0 && dm.NhomDanhMuc == 3).Select(dm => dm).ToList<DANHMUC>();
            ViewData["nhasanxuat"] = lstNSX;
            ViewData["danhmucnam"] = lstDMNam;
            ViewData["danhmucnu"] = lstDMNu;
            ViewData["danhmucphukien"] = lstDMPhuKien;
            return PartialView();
        }

        [HttpPost]
        public JsonResult GetProduct(string id)
        {
            SANPHAM product = new SANPHAM();
            product = db.SANPHAMs.SingleOrDefault(x => x.MaSP == id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}