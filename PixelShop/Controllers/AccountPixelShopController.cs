using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PixelShop.Controllers
{
    public class AccountPixelShopController : Controller
    {
        PixelShopEntities db = new PixelShopEntities();
        // GET: AccountPixelShop
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string email,string password)
        {
            MD5 md5Hash = MD5.Create();
            string pw = GetMd5Hash(md5Hash, password);
            TAIKHOAN tk = db.TAIKHOANs.Where(x => x.Email.Equals(email) && x.MatKhau.Equals(pw) && x.BiXoa==0).SingleOrDefault();
            if (tk == null)
            {
                TempData["UserMessage"] = new Message { CssClassName = "alert-danger", Title = "Thất bại!", MessageAlert = "Đăng nhập thất bai. Kiểm tra lại tài khoản và Đăng nhập lại." };
                return RedirectToAction("Index", "AccountPixelShop");
            }
            Session["username"] = tk.Email;
            Session["quyenhan"] = tk.QuyenHan;
            if (tk.QuyenHan == 1)
                return RedirectToAction("Index", "Admin");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session["username"] = null;
            Session["quyenhan"] = null;
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

        [HttpPost]
        public async Task<ActionResult> ForgetPassword(FormCollection fm)
        {
            if(fm["email"] == null)
            {
                TempData["UserMessage"] = new Message { CssClassName = "alert-danger", Title = "Thất bại!", MessageAlert = "Email không tồn tại" };
                return RedirectToAction("Index", "AccountPixelShop");

            }
            else
            {
                string email = fm["email"].ToString();
                int result = db.TAIKHOANs.Where(a => a.Email.Equals(email)).Count();
                if(result > 0)
                {
                    MD5 md5Hash = MD5.Create();
                    string dateNow = DateTime.Now.ToString("ddMMyyyyhhmmss");
                    TAIKHOAN tk = db.TAIKHOANs.Where(a => a.Email.Equals(email)).Single();
                    tk.token = GetMd5Hash(md5Hash, (email + dateNow));
                    int n = db.SaveChanges();
                    if (n > 0)
                    {
                      
                        return await SendEmail(email);
                    }
                    else
                    {
                        TempData["UserMessage"] = new Message { CssClassName = "alert-danger", Title = "Thất bại!", MessageAlert = "Có lỗi xảy ra vui lòng thử lại" };
                    }
                }
            }

            return RedirectToAction("Index", "AccountPixelShop"); 
            
        }

        private async Task<ActionResult> SendEmail(string email)
        {
            string ToEmail = email;
            TAIKHOAN tk = db.TAIKHOANs.Where(a => a.Email.Equals(email)).SingleOrDefault();
            if (tk != null) {
                var body = "<h4>From: PixelShop Admin</h4><p>Mã xác thực của bạn là:</p><p>" + tk.token + "</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(ToEmail));
                message.From = new MailAddress("donguyenminhluan96@gmail.com");
                message.Subject = "Thay đổi mật khẩu mới";
                message.Body = body;
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "pixelshop03@gmail.com",
                        Password = "minhluan"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);


                    TempData["sent"] = true;

                    return RedirectToAction("ReactivePassword", "AccountPixelShop");
                }
            }

            return RedirectToAction("Index", "AccountPixelShop");
        }



        
        public ActionResult ReactivePassword()
        {
            if(TempData["sent"]  != null)
            {
                if (bool.Parse(TempData["sent"].ToString()))
                {
                    TempData["UserMessage"] = new Message { CssClassName = "alert-success", Title = "Thành công!", MessageAlert = "Mã kích hoạt đã được gửi về email của bạn" };
                    TempData.Remove("sent");
                }
            }
            
            return View();
        }

        public ActionResult UpdateNewPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckUpdatePassWord(FormCollection fm)
        {
            if(TempData["email"] != null)
            {
                string email = TempData["email"].ToString();
                string mk = fm["password_new"].ToString();
                TAIKHOAN tk = db.TAIKHOANs.Where(a => a.Email.Equals(email)).SingleOrDefault();
                if(tk != null)
                {
                    MD5 md5Hash = MD5.Create();
                    tk.MatKhau = GetMd5Hash(md5Hash, mk);
                    tk.token = "";
                    int result = db.SaveChanges();
                    if(result > 0)
                    {
                        
                        TempData.Remove("email");
                        Session["username"] = null;
                        TempData["UserMessage"] = new Message { CssClassName = "alert-success", Title = "Thành công!", MessageAlert = "Mật khẩu đã được cập nhật. Vui lòng đăng nhập lại" };
                        return RedirectToAction("Index", "AccountPixelShop");
                    }
                    else
                    {
                        
                        TempData.Remove("email");
                        Session["username"] = null;
                        TempData["UserMessage"] = new Message { CssClassName = "alert-danger", Title = "Thất bại!", MessageAlert = "Có lỗi xảy ra. Vui lòng thử lại sau" };
                    }
                    
                } 
            }
            return RedirectToAction("Index", "AccountPixelShop");
        }

        [HttpPost]
        public ActionResult CompareToken(FormCollection fm)
        {
            if(fm["token"] == null)
            {
                return RedirectToAction("ReactivePassword", "AccountPixelShop");
            }
            string token = fm["token"].ToString();
            TAIKHOAN tk = db.TAIKHOANs.Where(a => a.token.Equals(token)).SingleOrDefault();
            if(tk != null)
            {
                TempData["email"] = tk.Email;
                return RedirectToAction("UpdateNewPassword", "AccountPixelShop");
            }
            return RedirectToAction("ReactivePassword", "AccountPixelShop");
        }

        public ActionResult Signup(string fullname,string email,string password)
        {
            TAIKHOAN check = db.TAIKHOANs.Where(x => x.Email.Equals(email)).SingleOrDefault();
            if (check != null)
            {
                TempData["UserMessage"] = new Message { CssClassName = "alert-danger", Title = "Thất bại!", MessageAlert = "Email này đã tồn tại." };
                return RedirectToAction("Index", "AccountPixelShop");
            }
            TAIKHOAN tk = new TAIKHOAN();
            tk.Email = email;
            tk.QuyenHan = 0;
            tk.HoTen = fullname;
            tk.BiXoa = 0;
            tk.token = "";
            MD5 md5Hash = MD5.Create();
            tk.MatKhau = GetMd5Hash(md5Hash, password);
            db.TAIKHOANs.Add(tk);
            int n = db.SaveChanges();
            if (n > 0)
            {
                TempData["UserMessage"] = new Message { CssClassName = "alert-success", Title = "Thành công!", MessageAlert = "Đã đăng kí tài khoản thành công." };
            }
            else
            {
                TempData["UserMessage"] = new Message { CssClassName = "alert-danger", Title = "Thất bại!", MessageAlert = "Xảy ra lỗi." };
            }
            return RedirectToAction("Index", "AccountPixelShop");
        }
    }
}