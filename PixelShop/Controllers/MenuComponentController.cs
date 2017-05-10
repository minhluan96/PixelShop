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
            List<DANHMUC> lstDM = db.DANHMUCs.Where(dm => dm.BiXoa == 0).Select(dm => dm).ToList<DANHMUC>();
            ViewData["nhasanxuat"] = lstNSX;
            ViewData["danhmuc"] = lstDM;

            return PartialView();
        }
    }
}