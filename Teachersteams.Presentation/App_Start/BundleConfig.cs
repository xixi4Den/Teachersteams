using System.Web.Optimization;

namespace Teachersteams
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/mainlibs")
                .Include("~/Scripts/libs/modernizr-*")
                .Include("~/Scripts/libs/angular/angular.js")
                .Include("~/Scripts/require.js")
                .Include("~/Scripts/r.js"));

            bundles.Add(new ScriptBundle("~/bundles/libs")
                .Include("~/Scripts/libs/ui-bootstrap-tpls-*")
                .Include("~/Scripts/libs/angular/switchery.js")
                .Include("~/Scripts/libs/angular/ng-switchery.js")
                .Include("~/Scripts/libs/angular/angular-animate.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .IncludeDirectory("~/Scripts/services", "*.js")
                .IncludeDirectory("~/Scripts/controllers", "*.js")
                .IncludeDirectory("~/Scripts/directives", "*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/switchery.css"));
        }
    }
}
