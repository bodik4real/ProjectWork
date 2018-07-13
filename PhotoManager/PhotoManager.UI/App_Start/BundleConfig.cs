using System.Web;
using System.Web.Optimization;

namespace PhotoManager.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/like").Include("~/Scripts/custom/like.js"));
            bundles.Add(new ScriptBundle("~/bundles/comment").Include("~/Scripts/custom/comment.js"));
            bundles.Add(new ScriptBundle("~/bundles/checkFile").Include("~/Scripts/custom/checkFile.js"));
            bundles.Add(new ScriptBundle("~/bundles/Preview").Include("~/Scripts/custom/Preview.js"));
            bundles.Add(new ScriptBundle("~/bundles/SetCurrImageName").Include("~/Scripts/custom/SetCurrImageName.js"));
        }
    }
}
