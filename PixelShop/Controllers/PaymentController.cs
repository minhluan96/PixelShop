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
            return View();
        }

        public ActionResult GhiNhanDonHang(FormCollection form)
        {
            DateTime dtNow = DateTime.Now;

            Random rand = new Random();
            int randNumb = rand.Next(10, 100);

            string madhag = dtNow.ToString("yyyyMMddhhss") + randNumb + "";
            DONHANG dh = new DONHANG()
            {
                MaDH = madhag,
                NgayDate = dtNow,
                NgayGiao = dtNow.AddDays(3),
                TinhTrang = 3,
                EmailDat = "admin@deptrai",
                DiaChiGiao = form["diaChi"],
                TenNguoiNhan = form["tenngnhan"],
                SDTNhan = form["sodt"]
            };

            if (db.DONHANGs.Where(p => p.MaDH.Equals(madhag)).Count() == 0)
            {
                db.DONHANGs.Add(dh);

                db.SaveChanges();
                themChiTietDH(madhag);
            }

            return RedirectToAction("ThanhToanThanhCong", "Payment", new { madh = madhag });
        }

        private void themChiTietDH(string madh)
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

                SANPHAM sp = db.SANPHAMs.Where(p => p.MaSP.Equals(item.Sanpham.MaSP)).SingleOrDefault();
                if(sp != null)
                {
                    if (sp.SoLuongTon >= item.Soluong)
                    {
                        sp.SoLuongTon = sp.SoLuongTon - item.Soluong;
                        sp.SoLuongBan = sp.SoLuongBan + item.Soluong;
                    }
                }

                lstCTDH.Add(ct);
            }

            db.CHITIETDONHANGs.AddRange(lstCTDH);
            db.SaveChanges();
        }

        public ActionResult ThanhToanThanhCong(string madh)
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