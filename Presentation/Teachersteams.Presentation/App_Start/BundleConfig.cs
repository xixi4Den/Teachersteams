using System.Web.Optimization;

namespace Teachersteams.Presentation
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/libs")
                .Include("~/Scripts/libs/angular/angular.js")
                .Include("~/Scripts/libs/angular/angular-ui-router.js")
                .Include("~/Scripts/libs/angular/angular-animate.js")
                .Include("~/Scripts/libs/angular/angular-sanitize.js")
                .Include("~/Scripts/libs/angular/angular-messages.js")
                .Include("~/Scripts/libs/ui-bootstrap-tpls-*")
                .Include("~/Scripts/libs/switchery.js")
                .Include("~/Scripts/libs/ng-switchery.js")
                .Include("~/Scripts/libs/ngDialog.js")
                .Include("~/Scripts/libs/ngToast.js")
                .Include("~/Scripts/libs/ui-grid.js")
                .Include("~/Scripts/libs/angular-file-upload.js")
                .Include("~/Scripts/libs/underscore.js")
                .Include("~/Scripts/libs/intro.js")
                .Include("~/Scripts/libs/angular-intro.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .Include("~/Scripts/modules.js")
                .Include("~/Scripts/singlePageApp.js")
                .IncludeDirectory("~/Scripts/services", "*.js")
                .IncludeDirectory("~/Scripts/controllers", "*.js", true)
                .IncludeDirectory("~/Scripts/directives", "*.js")
                .IncludeDirectory("~/Scripts/enums", "*.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/colors.less")
                .Include("~/Content/bootstrap/bootstrap.css")
                .Include("~/Content/site.less")
                .Include("~/Content/transitions.less")
                .Include("~/Content/switchery/switchery.css")
                .Include("~/Content/loader.css")
                .IncludeDirectory("~/Content/ngDialog", "*.css")
                .IncludeDirectory("~/Content/ngToast", "*.css")
                .Include("~/Content/ui-grid/ui-grid.css")
                .Include("~/Content/introjs.css"));
        }
    }
}
