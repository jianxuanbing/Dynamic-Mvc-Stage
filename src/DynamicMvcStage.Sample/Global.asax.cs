using DynamicMvcStage.Core.DependencyInjection;
using DynamicMvcStage.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Optimization;
using System.Web.Routing;

namespace DynamicMvcStage.Sample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IContainer container = new Container();
            container.RegisterDynamicMvcCore();
            container.RegisterDynamicMvcController<ITestService , TestService>();
            DependencyResolver.SetResolver(container.TryResolve , type => new object[] { });
        }
    }
}
