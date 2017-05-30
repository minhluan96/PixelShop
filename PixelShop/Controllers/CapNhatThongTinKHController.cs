using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            
            if(Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string mailkh = Session["username"].ToString();
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
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
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
                TempData["UserMessage"] = new Message { CssClassName = "alert-danger", Title = "Thất bại!", MessageAlert = "Tài khoản này không tồn tại." };
                return RedirectToAction("Index", "Home");
            }
            MD5 md5Hash = MD5.Create();
            tk.HoTen = hoten;
            tk.MatKhau = GetMd5Hash(md5Hash, password);

            int n = db.SaveChanges();
            if (n > 0)
            {
                TempData["UserMessage"] = new Message { CssClassName = "alert-success", Title = "Thành công!", MessageAlert = "Đã cập nhật thông tin cá nhân thành công." };
                TempData["capnhat"] = 1;
            }
            else
            {
                TempData["UserMessage"] = new Message { CssClassName = "alert-danger", Title = "Thất bại!", MessageAlert = "Thông tin không thay đổi. Vui lòng nhập thông tin mới." };
                TempData["capnhat"] = 0;
            }

            return RedirectToAction("Index", "CapNhatThongTinKH");
        }
    }
}