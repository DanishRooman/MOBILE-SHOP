using System.Web;
using System.Web.Optimization;

namespace MOBILESHOP
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/jquery-1.10.2.min.js",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/Script/unobstrasive").Include(
              "~/Scripts/jquery.unobtrusive-ajax.js",
              "~/Scripts/jquery.validate.min.js"

             ));

            bundles.Add(new ScriptBundle("~/Script/unobtrusive-script").Include(
                "~/Scripts/jquery.validate.unobtrusive.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                       "~/assets/script.js"));

            bundles.Add(new ScriptBundle("~/bundles/owl.carousel").Include(
                      "~/assets/owl-carousel/owl.carousel.js"));

            bundles.Add(new ScriptBundle("~/bundles/slitslider").Include(
                      "~/assets/slitslider/js/modernizr.custom.79639.js",
                      "~/assets/slitslider/js/jquery.ba-cond.min.js",
                      "~/assets/slitslider/js/jquery.slitslider.js"));

            bundles.Add(new ScriptBundle("~/bundles/Toast-and-Confirm").Include(
                "~/assets/owl-carousel/owl.carousel.js",
                "~/Scripts/jquery.toast.min.js",
                "~/Scripts/jquery-confirm.js",
                "~/Scripts/select2.min.js",
                "~/fontawesome/js/fontawesome.js",
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap.min.js"
                ));


            //----------------------- CSS ------------------------//


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/jquery-confirm.css",
                "~/Content/jquery.toast.min.css",
                "~/assets/style.css",
                "~/Content/select2.min.css",
                "~/fontawesome/css/fontawesome.css"
               ));



            bundles.Add(new StyleBundle("~/Content/owl-carousel-css").Include(
                     "~/assets/owl-carousel/owl.carousel.css",
                     "~/assets/owl-carousel/owl.theme.css"
                     ));
            bundles.Add(new StyleBundle("~/Content/slitslider-css").Include(
                     "~/assets/slitslider/css/style.css",
                     "~/assets/slitslider/css/custom.css"
                     ));
            bundles.Add(new StyleBundle("~/Contents/Datatable-style").Include(
               "~/Content/dataTables.bootstrap.min.css",
               "~/Content/responsive.bootstrap.min.css"
               ));

        }
    }
}
