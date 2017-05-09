using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class TimKiemController : Controller
    {

        PixelShopEntities db = new PixelShopEntities();
        // GET: ListProduct
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NhaSanXuat(string id)
        {
            List<SANPHAM> dsSanPham = db.SANPHAMs.Where(sp => sp.BiXoa == 0).Select(sp => sp).ToList<SANPHAM>();
            if (!String.IsNullOrEmpty(id))
            {
                dsSanPham = db.SANPHAMs.Where(sp => sp.NhaSanXuat.Equals(id) && sp.BiXoa == 0).Select(sp => sp).ToList<SANPHAM>();
                return View(dsSanPham);
            }
            return View(dsSanPham);
        }

        public ActionResult DanhMuc(string id)
        {
            List<SANPHAM> lstSPDanhMuc = db.SANPHAMs.Where(sp => sp.BiXoa == 0).Select(sp => sp).ToList<SANPHAM>();
            if (!String.IsNullOrEmpty(id))
            {
                lstSPDanhMuc = db.SANPHAMs.Where(sp => sp.DanhMuc.Equals(id) && sp.BiXoa == 0).Select(sp => sp).ToList<SANPHAM>();
                return View(lstSPDanhMuc);
            }
            return View(lstSPDanhMuc);
        }
        
    }
}