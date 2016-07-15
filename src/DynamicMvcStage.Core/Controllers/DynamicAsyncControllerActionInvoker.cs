using DynamicMvcStage.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace DynamicMvcStage.Core
{
    public class DynamicAsyncControllerActionInvoker : AsyncControllerActionInvoker
    {
        private readonly IDynamicControllerTypeFactory dynamicControllerTypeFactory;
        public DynamicAsyncControllerActionInvoker(IDynamicControllerTypeFactory dynamicControllerTypeFactory)
        {
            this.dynamicControllerTypeFactory = dynamicControllerTypeFactory;
        }
        protected override ControllerDescriptor GetControllerDescriptor(ControllerContext controllerContext)
        {
            DynamicControllerContext dynamicControllerContext = new DynamicControllerContext((string)controllerContext.RouteData.Values["controller"]);
            Type dynamicControllerType = dynamicControllerTypeFactory.CreateControllerType(dynamicControllerContext);
            controllerContext.Controller = dynamicControllerTypeFactory.CreateController(dynamicControllerContext);
            return new ReflectedControllerDescriptor(dynamicControllerType);
        }
    }
}
