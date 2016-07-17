using DynamicMvcStage.Core.DependencyInjection;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Routing;
using System.Web.SessionState;
using DynamicMvcStage.Core.Controllers.Builders;

namespace DynamicMvcStage.Core.Controllers
{
    public class DynamicControllerFactory : DefaultControllerFactory
    {
        private readonly IDynamicControllerContextManager dynamicControllerContextManager;
        private readonly IDynamicControllerTypeBuilder controllerTypeBuilder;

        public DynamicControllerFactory(IDynamicControllerTypeBuilder controllerTypeBuilder , IDynamicControllerContextManager dynamicControllerContextManager)
        {
            this.dynamicControllerContextManager = dynamicControllerContextManager;
            this.controllerTypeBuilder = controllerTypeBuilder;
        }

        protected override Type GetControllerType(RequestContext requestContext , string controllerName)
        {
            DynamicControllerContext dynamicControllerContext = dynamicControllerContextManager.GetContext(controllerName);
            if (dynamicControllerContext == null) return base.GetControllerType(requestContext , controllerName);
            return controllerTypeBuilder.BuildControllerType(dynamicControllerContext);
        }
    }
}
