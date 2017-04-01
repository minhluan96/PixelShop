using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class MenuComponentController : Controller
    {
        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            return PartialView();
        }
    }
}