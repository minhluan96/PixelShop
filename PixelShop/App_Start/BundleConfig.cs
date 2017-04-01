using System.Web;
using System.Web.Optimization;

namespace PixelShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery-1.11.1.min.js",
                        "~/Scripts/jquery.flexisel.js",
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                        "~/Content/style.css",
                        "~/Content/font-awesome.min.css"));
        }
    }
}
