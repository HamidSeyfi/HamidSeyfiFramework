using HSF.BaseSystemModel.Helper;
using HSF.Business.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HSF
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConnectionString.SqlServerConnectionString = @"data source=DESKTOP-THBF5B2\MSSQLSERVER2016;initial catalog=HamidSeyfiFramework;integrated security=True";
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();

            var errorMsg = JsonConvert.SerializeObject(new { url = HttpContext.Current.Request.Url.ToString(), error.Message, error.InnerException, error.StackTrace });

            LogBiz.Log("Application_Error", errorMsg, LogType.Exception);

            Response.Clear(); // to ensure that any content written to the response stream before the error occurred is removed.

            if (HttpContext.Current.IsDebuggingEnabled == false)
            {
                Server.ClearError();// to stop ASP.NET from serving the yellow screen of death.
                Response.Redirect("~/Error/Index");
            }


        }
    }
}
