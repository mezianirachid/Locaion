using System.Web;
using System.Web.Optimization;

namespace Location
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js","~/Scripts/respond.js","~/Scripts/Datatables/jquery.dataTables.min.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css","~/Content/site.css","~/Content/Datatables/jquery.dataTables.min.css"));

            //bundles.Add(new ScriptBundle("~/bundles/datePicker").Include( "~/Scripts/moment.min.js","~/Scripts/bootstrap-datetimepicker.min.js"));

            //bundles.Add(new StyleBundle("~/Content/datepicker").Include( "~/Content/bootstrap-datetimepicker.min.css"));


            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Content/jQuery-Mask-Plugin-master/dist/jquery.mask.min.js"));


            //BundleTable.EnableOptimizations = true;

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

            // jquery datataables js files
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                        "~/Scripts/DataTables/dataTables.bootstrap.js"));

            // jquery datatables css file
            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                      "~/Content/DataTables/css/dataTables.bootstrap.css"));


            bundles.Add(new ScriptBundle("~/bundles/jquerymask").Include(
               "~/Scripts/jquery.maskedinput*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymask").Include(
                "~/Scripts/jquery.maskedinput*",
                "~/Scripts/maskedinput-binder.js"));


            BundleTable.EnableOptimizations = true;
        }
    }
}
