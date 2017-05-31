using PagedList;
using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class PurchaseHistoryController : Controller
    {
        // GET: LishSuMuaHang
        PixelShopEntities db = new PixelShopEntities();
        public ActionResult Index(int ?page)
        {
            
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string maNgDat = Session["username"].ToString();
            List<DONHANG> dsDH = db.DONHANGs.Where(d => d.EmailDat.Equals(maNgDat)).Select(d => d).ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(dsDH.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DetailPurchaseHistory(string maDH, int? page)
        {
            List<CHITIETDONHANG> dsCT = db.CHITIETDONHANGs.Where(c => c.MaDH.Equals(maDH)).Select(c => c).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(dsCT.ToPagedList(pageNumber, pageSize));
        }
    }
}