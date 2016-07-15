using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace DynamicMvcStage.Web.__test
{
    public class StageAsyncActionInvoker: AsyncControllerActionInvoker
    {
        protected override ControllerDescriptor GetControllerDescriptor(ControllerContext controllerContext)
        {
            var route = controllerContext.RouteData;
            return base.GetControllerDescriptor(controllerContext);
        }
    }
}