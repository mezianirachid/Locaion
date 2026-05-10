using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Globalization;
using System.Threading;
using System.Web.Security;
 

namespace Location
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            var culture = new System.Globalization.CultureInfo("en-CA");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            if (FormsAuthentication.RequireSSL && !Request.IsSecureConnection)
            {
                Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            // Définir la culture par défaut pour toute l'application
            var culture = new System.Globalization.CultureInfo("fr-CA");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;

             

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);                      
        
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }

        
    }
}
