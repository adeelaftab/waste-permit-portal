using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using HFZMVC.App_Start;
using System.Web.Optimization;
using HFZMVC.AppLogics;
using SAP.Middleware.Connector;

namespace HFZMVC
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            SAPConfig cfg = null;
            // Code that runs on application startup
            cfg = new SAPConfig();
            RfcDestinationManager.RegisterDestinationConfiguration(cfg);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_End(object sender, EventArgs e)
        {
            SAPConfig cfg = null;
            // Code that runs on application startup
            cfg = new SAPConfig();
            RfcDestinationManager.UnregisterDestinationConfiguration(cfg);
        }
        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            //log the error!
            if (ex != null)
            {
                AppUtil.ExceptionLog(ex);
                //Server.ClearError();
                //Server
                //  .Transfer("~/Error/Index");
            }

        }
    }
}