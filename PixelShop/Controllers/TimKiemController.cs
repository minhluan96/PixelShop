using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class TimKiemController : Controller
    {

        PixelShopEntities db = new PixelShopEntities();
        // GET: ListProduct
        public ActionResult Index(ProductSearchModel searchModel, int? page)
        {
            List<NHASANXUAT> lstNSX = db.NHASANXUATs.Where(nsx => nsx.BiXoa == 0).Select(nsx => nsx).ToList<NHASANXUAT>();
            List<DANHMUC> lstDMNam = db.DANHMUCs.Where(dm => dm.BiXoa == 0 && dm.NhomDanhMuc == 1).Select(dm => dm).ToList<DANHMUC>();
            List<DANHMUC> lstDMNu = db.DANHMUCs.Where(dm => dm.BiXoa == 0 && dm.NhomDanhMuc == 2).Select(dm => dm).ToList<DANHMUC>();
            List<DANHMUC> lstDMPhuKien = db.DANHMUCs.Where(dm => dm.BiXoa == 0 && dm.NhomDanhMuc == 3).Select(dm => dm).ToList<DANHMUC>();
            ViewData["danhmucnam"] = lstDMNam;
            ViewData["danhmucnu"] = lstDMNu;
            ViewData["danhmucphukien"] = lstDMPhuKien;
            ViewData["nhasanxuat"] = lstNSX;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var business = new ProductBusinessLogic();
            return View(business.GetProducts(searchModel).ToList<SANPHAM>().ToPagedList(pageNumber, pageSize));
        }        
    }
}