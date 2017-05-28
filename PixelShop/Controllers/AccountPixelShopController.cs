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
    public class AccountPixelShopController : Controller
    {
        PixelShopEntities db = new PixelShopEntities();
        // GET: AccountPixelShop
        public ActionResult Index()
        {
            return View(@"~/Views/Login/Index.cshtml");
        }
        public ActionResult Login(string email,string password)
        {
            MD5 md5Hash = MD5.Create();
            string pw = GetMd5Hash(md5Hash, password);
            TAIKHOAN tk = db.TAIKHOANs.Where(x => x.Email.Equals(email) && x.MatKhau.Equals(pw)).SingleOrDefault();
            if(tk==null)
                return RedirectToAction("Index", "AccountPixelShop");
            Session["username"] = tk.Email;
            if (tk.QuyenHan == 1)
                RedirectToAction("Index", "Admin");
            return RedirectToAction("Index", "Home");
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
        public ActionResult Signup(string fullname,string email,string password)
        {
            TAIKHOAN tk = new TAIKHOAN();
            tk.Email = email;
            tk.QuyenHan = 0;
            tk.HoTen = fullname;
            tk.BiXoa = 0;
            MD5 md5Hash = MD5.Create();
            tk.MatKhau = GetMd5Hash(md5Hash, password);
            db.TAIKHOANs.Add(tk);
            db.SaveChanges();
            return View(@"~/Views/Login/Index.cshtml");
        }
    }
}