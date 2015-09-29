using System.Web;
using System.Web.Optimization;

namespace AquaCultureMonitor
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/canvasjs").Include(
                      "~/Scripts/jquery.canvasjs.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/material").Include(
                      "~/Content/material-design/roboto.min.css",
                      "~/Content/material-design/material.min.css",
                      "~/Content/material-design/ripples.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/material").Include(
                      "~/Scripts/material-design/ripples.min.js",
                      "~/Scripts/material-design/material.min.js",
                      "~/Scripts/application.js"));

        }
    }
}
