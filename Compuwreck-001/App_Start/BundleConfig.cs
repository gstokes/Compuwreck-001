using System.Web;
using System.Web.Optimization;

namespace Compuwreck_001
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/ScriptsJS").Include(
                "~/Scripts/foundation/foundation.js",
                "~/Scripts/foundation/foundation.reveal.js",
                "~/Scripts/vendor/responsive-nav.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                      "~/Content/css/foundation.css",
                      "~/Content/css/responsive-nav.css",
                      "~/Content/css/navStyles.css"
                      ));
        }
    }
}
