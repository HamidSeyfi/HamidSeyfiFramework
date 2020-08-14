using System.Web;
using System.Web.Optimization;

namespace HSF
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region js
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));


            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));




            bundles.Add(new ScriptBundle("~/bundles/siteJs").Include(
                      "~/Scripts/helper.js"));

            #endregion


            #region css

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                     "~/Content/bootstrap.css"));


            bundles.Add(new StyleBundle("~/Content/siteCss").Include(
                   "~/Content/font.css",
                   "~/Content/helper.css",
                   "~/Content/site.css"));

            #endregion
        }
    }
}
