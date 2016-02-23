using System.Web.Optimization;

namespace Teachersteams
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/libs")
                //.Include("~/Scripts/libs/modernizr-*")
                .Include("~/Scripts/libs/angular/angular.js")
                .Include("~/Scripts/libs/angular/angular-ui-router.js")
                .Include("~/Scripts/libs/angular/angular-animate.js")
                .Include("~/Scripts/libs/ui-bootstrap-tpls-*")
                .Include("~/Scripts/libs/switchery.js")
                .Include("~/Scripts/libs/ng-switchery.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .Include("~/Scripts/singlePageApp.js")
                .IncludeDirectory("~/Scripts/services", "*.js")
                .IncludeDirectory("~/Scripts/controllers", "*.js")
                .IncludeDirectory("~/Scripts/directives", "*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/colors.less",
                "~/Content/bootstrap/bootstrap.css",
                "~/Content/site.less",
                "~/Content/switchery.css"));
        }
    }
}
