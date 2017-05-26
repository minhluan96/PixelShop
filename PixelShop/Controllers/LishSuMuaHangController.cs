using PagedList;
using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class LishSuMuaHangController : Controller
    {
        // GET: LishSuMuaHang
        PixelShopEntities db = new PixelShopEntities();
        public ActionResult Index(int ?page)
        {
            string maNgDat = "admin@deptrai";
            List<DONHANG> dsDH = db.DONHANGs.Where(d => d.EmailDat.Equals(maNgDat)).Select(d => d).ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(dsDH.ToPagedList(pageNumber, pageSize));
        }
    }
}