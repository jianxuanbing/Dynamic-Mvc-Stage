using DynamicMvcStage.Core.DependencyInjection;
using DynamicMvcStage.Web.__test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Optimization;
using System.Web.Routing;

namespace DynamicMvcStage.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Container container = new Container();
            container.Register<IControllerFactory , DefaultControllerFactory>();
            container.Register<IAsyncActionInvoker , StageAsyncActionInvoker>();
            DependencyResolver.SetResolver(new StageDependencyResolve(container));
        }
    }
}
