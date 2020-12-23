using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HFZMVC.App_Start
{
  public class BundleConfig
  {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles) {

      BundleTable.EnableOptimizations = false;
      //All Javascripts
      bundles.Add(new ScriptBundle("~/Assets/javascripts").Include(
                  "~/Assets/js/jquery.min.js",
                  "~/Assets/js/bootstrap.bundle.min.js",
                  "~/Assets/js/metisMenu.min.js",
                  "~/Assets/js/jquery.slimscroll.js",
                  "~/Assets/js/waves.min.js" ,
                  "~/Assets/plugins/datatables/jquery.dataTables.min.js",
                  "~/Assets/plugins/datatables/dataTables.bootstrap4.min.js",
                  "~/Assets/plugins/datatables/dataTables.buttons.min.js",
                  "~/Assets/plugins/datatables/buttons.bootstrap4.min.js",
                  "~/Assets/plugins/datatables/jszip.min.js",
                  "~/Assets/plugins/datatables/pdfmake.min.js",
                  "~/Assets/plugins/datatables/vfs_fonts.js",
                  "~/Assets/plugins/datatables/buttons.html5.min.js",
                  "~/Assets/plugins/datatables/buttons.print.min.js",
                  "~/Assets/plugins/datatables/buttons.colVis.min.js",
                  "~/Assets/js/app.js",
                  "~/Assets/js/bootstrap-datepicker.js",
                  "~/Assets/pages/datatables.init.js",
                  "~/Assets/js/select2.min.js",
                  "~/Assets/js/jquery.smartWizard.js",
                  
                  "~/Assets/js/bootbox.all.min.js"
        ));

      #region --------------------------Dashboard Bundles--------------------------------------
      bundles.Add(new ScriptBundle("~/Assets/dashboards").Include(
        "~/Assets/plugins/jquery-sparkline/jquery.sparkline.min.js",
        "~/Assets/plugins/raphael/raphael-min.js",
         "~/Assets/pages/dashboard.js"));

      
      #endregion

      //All applicatoin CSS
     
      bundles.Add(new StyleBundle("~/Assets/css").Include(
                "~/Assets/plugins/datatables/dataTables.bootstrap4.min.css",
                "~/Assets/plugins/datatables/buttons.bootstrap4.min.css",
                "~/Assets/plugins/datatables/responsive.bootstrap4.min.css",
                "~/Assets/css/bootstrap.min.css",
                "~/Assets/css/smart_wizard.css",
                "~/Assets/css/smart_wizard_theme_circles.css",
                "~/Assets/css/metismenu.min.css",
                "~/Assets/css/icons.css",
                "~/Assets/css/select2.min.css",
                "~/Assets/css/bootstrap-datepicker.css",
                "~/Assets/css/style.css"

                ));
    }
  }
}