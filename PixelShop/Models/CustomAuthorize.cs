using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Models
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Now check the session:
            var myvar = httpContext.Session["quyenhan"];
            if (myvar == null || (int)myvar == 0)
            {
                // the session has expired
                return false;
            }

            return true;
        }
    }
}