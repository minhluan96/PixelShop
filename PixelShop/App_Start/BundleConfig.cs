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
                        "~/Scripts/jquery.flexisel.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/easyResponsiveTabs.js",
                        "~/Scripts/jquery.flexslider.js",
                        "~/Scripts/jquery.wmuSlider.js",
                        "~/Scripts/forIndex.js",
                        "~/Scripts/forInfo.js",
                        "~/Scripts/checkout.js",
                        "~/Scripts/about.js",
                        "~/Scripts/imagezoom.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrapCheckout.css",
                        "~/Content/style.css",
                        "~/Content/checkout.css",
                        "~/Content/font-awesome.min.css",
                        "~/Content/creditly.css",
                        "~/Content/creditly.css",
                        "~/Content/creditly.css",
                        "~/Content/flexslider.css"));


            bundles.Add(new StyleBundle("~/Content/css-payment").Include(
                        "~/Content/creditly.css",
                        "~/Content/easy-responsive-tabs.css",
                        "~/Content/Payment-style.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts-payment").Include(
                        "~/Scripts/creditly.js",
                         "~/Scripts/easy-responsive-tabs.js",
                          "~/Scripts/jquery.min.js"
                       ));

            bundles.Add(new StyleBundle("~/Content/css-login").Include(
                       "~/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                        "~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                        "~/assets/global/plugins/uniform/css/uniform.default.css",
                        "~/assets/admin/pages/css/login.css",
                        "~/assets/global/css/components-rounded.css",
                        "~/assets/global/css/plugins.css",
                        "~/assets/admin/layout4/css/layout.css",
                        "~/assets/admin/layout4/css/themes/default.css",
                        "~/assets/admin/layout4/css/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts-login").Include(
                        "~/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                        "~/assets/global/plugins/jquery.blockui.min.js",
                        "~/assets/global/plugins/uniform/jquery.uniform.min.js",
                        "~/assets/global/plugins/jquery.cokie.min.js",
                        "~/assets/global/scripts/metronic.js",
                        "~/assets/admin/layout4/scripts/layout.js",
                        "~/assets/admin/layout4/scripts/demo.js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptsAdminIndex").Include(
                       "~/assets/global/plugins/jquery.min.js",
                        "~/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                        "~/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                        "~/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/assets/global/plugins/jquery.blockui.min.js",
                        "~/assets/global/plugins/jquery.cokie.min.js",
                        "~/assets/global/plugins/uniform/jquery.uniform.min.js",
                        "~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                        "~/assets/global/plugins/flot/jquery.flot.js",
                        "~/assets/global/plugins/flot/jquery.flot.resize.js",
                        "~/assets/global/plugins/flot/jquery.flot.categories.js",
                        "~/assets/global/scripts/metronic.js",
                        "~/assets/admin/layout4/scripts/layout.js",
                        "~/assets/admin/layout4/scripts/demo.js",
                        "~/assets/admin/pages/scripts/ecommerce-index.js",
                        "~/assets/global/plugins/forAdmin.js"));

            bundles.Add(new StyleBundle("~/Content/cssAdminIndex").Include(
                        "~/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                        "~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                        "~/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                        "~/assets/global/plugins/uniform/css/uniform.default.css",
                        "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                        "~/assets/global/css/components-rounded.css",
                        "~/assets/global/css/plugins.css",
                        "~/assets/admin/layout4/css/layout.css",
                        "~/assets/admin/layout4/css/themes/light.css",
                        "~/assets/admin/layout4/css/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/scriptsForAdmin").Include(
                        "~/assets/global/plugins/jquery.min.js",
                        "~/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                        "~/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                        "~/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/assets/global/plugins/jquery.blockui.min.js",
                        "~/assets/global/plugins/jquery.cokie.min.js",
                        "~/assets/global/plugins/uniform/jquery.uniform.min.js",
                        "~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                        "~/assets/global/scripts/metronic.js",
                        "~/assets/admin/layout4/scripts/layout.js",
                        "~/assets/admin/layout4/scripts/demo.js",
                        "~/assets/global/scripts/datatable.js",
                        "~/assets/global/plugins/select2/select2.min.js",
                        "~/assets/global/plugins/datatables/media/js/jquery.dataTables.min.js",
                        "~/assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js",
                        "~/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                        "~/assets/global/plugins/forAdmin.js"));

            bundles.Add(new StyleBundle("~/Content/cssForAdmin").Include(
                        "~/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                        "~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                        "~/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                        "~/assets/global/plugins/uniform/css/uniform.default.css",
                        "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                        "~/assets/global/plugins/select2/select2.css",
                        "~/assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css",
                        "~/assets/global/plugins/bootstrap-datepicker/css/datepicker.css",
                        "~/assets/global/css/components-rounded.css",
                        "~/assets/global/css/plugins.css",
                        "~/assets/admin/layout4/css/layout.css",
                        "~/assets/admin/layout4/css/themes/light.css",
                        "~/assets/admin/layout4/css/custom.css",
                        "~/Content/div-iframeAdmin.css"));

        }
    }
}
