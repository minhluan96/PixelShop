using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View(@"~/Views/Admin/Order.cshtml");
        }
        public ActionResult Customer()
        {
            return View(@"~/Views/Admin/Customer.cshtml");
        }
        public ActionResult Product()
        {
            return View(@"~/Views/Admin/Product.cshtml");
        }
        public ActionResult Category()
        {
            return View(@"~/Views/Admin/Category.cshtml");
        }
        public ActionResult Manufacturer()
        {
            return View(@"~/Views/Admin/Manufacturer.cshtml");
        }
        public PartialViewResult Header()
        {
            return PartialView(@"~/Views/Admin/Header.cshtml");
        }
        public PartialViewResult Sidebar()
        {
            return PartialView(@"~/Views/Admin/Sidebar.cshtml");
        }
        public PartialViewResult Footer()
        {
            return PartialView(@"~/Views/Admin/Footer.cshtml");
        }
    }
}