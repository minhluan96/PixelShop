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
            List<DANHMUC> lstDM = db.DANHMUCs.Where(dm => dm.BiXoa == 0).Select(dm => dm).ToList<DANHMUC>();
            ViewData["nhasanxuat"] = lstNSX;
            ViewData["danhmuc"] = lstDM;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var business = new ProductBusinessLogic();
            return View(business.GetProducts(searchModel).ToList<SANPHAM>().ToPagedList(pageNumber, pageSize));
        }        
    }
}