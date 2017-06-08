using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class PaymentController : Controller
    {
        PixelShopEntities db = new PixelShopEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if(Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult RecordOrders(FormCollection form)
        {
            if(Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            

            DateTime dtNow = DateTime.Now;

            Random rand = new Random();
            int randNumb = rand.Next(10, 100);

            string madhag = dtNow.ToString("yyyyMMddhhss") + randNumb + "";
            if(String.IsNullOrEmpty(form["diaChi"]) || 
                String.IsNullOrEmpty(form["tenngnhan"]) || 
                String.IsNullOrEmpty(form["sodt"])
                || String.IsNullOrWhiteSpace(form["diaChi"]) || String.IsNullOrWhiteSpace(form["tenngnhan"]) || String.IsNullOrWhiteSpace(form["sodt"]))
            {
                TempData["UserMessage"] = new Message { CssClassName = "alert-danger", Title = "Thất bại!", MessageAlert = "Vui lòng nhập đủ thông tin đơn hàng." };
                return RedirectToAction("Index", "Payment");
            }
            DONHANG dh = new DONHANG()
            {
                MaDH = madhag,
                NgayDate = dtNow,
                NgayGiao = null,
                TinhTrang = 3,
                EmailDat = Session["username"].ToString(),
                DiaChiGiao = form["diaChi"],
                TenNguoiNhan = form["tenngnhan"],
                SDTNhan = form["sodt"]
            };

            if (db.DONHANGs.Where(p => p.MaDH.Equals(madhag)).Count() == 0)
            {
                db.DONHANGs.Add(dh);

                db.SaveChanges();
                addDetailOrder(madhag);
            }

            return RedirectToAction("PaymentSuccess", "Payment", new { madh = madhag });
        }

        private void addDetailOrder(string madh)
        {
            List<CHITIETDONHANG> lstCTDH = new List<CHITIETDONHANG>();
            List<Item> lstItem = (List<Item>)Session["cart"];
            foreach(Item item in lstItem)
            {
                CHITIETDONHANG ct = new CHITIETDONHANG()
                {
                    MaDH = madh,
                    MaSP = item.Sanpham.MaSP,
                    SoLuongDat = item.Soluong,
                    GiaBan = item.Sanpham.GiaBan
                };

                

                lstCTDH.Add(ct);
            }

            db.CHITIETDONHANGs.AddRange(lstCTDH);
            db.SaveChanges();
        }

        public ActionResult PaymentSuccess(string madh)
        {
            var dh = db.DONHANGs.Where(p => p.MaDH.Equals(madh)).SingleOrDefault();
            
            if (dh != null)
            {
                DONHANG d = dh as DONHANG;


                Session["cart"] = null;
                return View(dh);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}