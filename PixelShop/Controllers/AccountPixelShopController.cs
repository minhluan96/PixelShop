using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class AccountPixelShopController : Controller
    {
        // GET: AccountPixelShop
        public ActionResult Index()
        {
            return View(@"~/Views/Login/Index.cshtml");
        }
    }
}