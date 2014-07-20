using System.Web;
using System.Web.Optimization;
using MongoDB.Driver.Linq;

namespace KO_Angular_Demo
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/libraries").Include(
                "~/Scripts/underscore.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/project").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/Knockout/Project/createprojectviewmodel.js",
                "~/Scripts/Knockout/Project/projectlistviewmodel.js",
                "~/Scripts/Knockout/Project/projectviewmodel.js"
                ));

             bundles.Add(new ScriptBundle("~/bundles/angularProject").Include("~/Scripts/angular.js",
                    "~/Scripts/angular-route.js",
                    "~/Scripts/angular-resource.js",
                    "~/Scripts/Angular/app.js",
                    "~/Scripts/Angular/routes.js",
                    "~/Scripts/Angular/Project/Controllers/ProjectListCtrl.js",
                    "~/Scripts/Angular/Project/Services/ProjectsService.js",
                    "~/Scripts/Angular/Project/Controllers/ProjectCreateCtrl.js",
                    "~/Scripts/Angular/Project/Controllers/ProjectCtrl.js",
                    "~/Scripts/Angular/Project/Controllers/TaskCtrl.js",
                    "~/Scripts/Angular/Project/Services/TasksService.js"
                 ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
