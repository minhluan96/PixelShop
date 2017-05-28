using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class CapNhatThongTinKHController : Controller
    {
        // GET: CapNhatThongTinKH
        PixelShopEntities db = new PixelShopEntities();

        [OutputCache(NoStore = true, Duration = 1)]
        public ActionResult Index()
        {
            //check session kh o day
            string mailkh = "admin@deptrai";
            TAIKHOAN tk = db.TAIKHOANs.Where(a => a.Email.Equals(mailkh)).SingleOrDefault();
            if(tk == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["capnhat"] != null) {
                ViewData["capnhat"] = 1;
                TempData.Remove("capnhat");
            }
            else
            {
                ViewData["capnhat"] = 0;
            }
            return View(tk);
        }

        [HttpPost]
        public ActionResult capNhatThongTin(FormCollection frm)
        {
            string hoten = frm["hoten"];
            string email = frm["email"];
            string password = frm["password_new"];
            TAIKHOAN tk = db.TAIKHOANs.Where(a => a.Email.Equals(email)).SingleOrDefault();

            if (tk == null)
            {
                return RedirectToAction("Index", "Home");
            }

            tk.HoTen = hoten;
            tk.MatKhau = password;
            db.SaveChanges();
            TempData["capnhat"] = 1;

            return RedirectToAction("Index", "CapNhatThongTinKH");
        }
    }
}