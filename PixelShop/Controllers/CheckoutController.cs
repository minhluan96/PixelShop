using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        PixelShopEntities db = new PixelShopEntities();
        public ActionResult Index()
        {
            return View();
        }
    }
}