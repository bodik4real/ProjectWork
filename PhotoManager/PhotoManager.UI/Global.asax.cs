using PhotoManager.UI.App_Start;
using SimpleInjector;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PhotoManager.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();

            SimpleInjectorConfig.Initialize(container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutomapperConfig.Initialize();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception is HttpException)
            {
                var httpException = (HttpException)exception;
                Response.StatusCode = httpException.GetHttpCode();
            }
        }
    }
}
