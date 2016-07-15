using DynamicMvcStage.Core.DependencyInjection;
using DynamicMvcStage.Sample.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace DynamicMvcStage.Sample.__test
{
    public class TestAsyncControllerActionInvoker: AsyncControllerActionInvoker
    {
        private readonly IContainer container;

        public TestAsyncControllerActionInvoker(IContainer container)
        {
            this.container = container;
        }

        protected override ControllerDescriptor GetControllerDescriptor(ControllerContext controllerContext)
        {
          
            controllerContext.Controller = (ControllerBase)container.Resolve(typeof(TestController));
            return new TestControllerDescriptor(typeof(TestController));
        }
    }

    public class TestControllerDescriptor : ReflectedControllerDescriptor
    {
        public TestControllerDescriptor(Type controllerType) : base(controllerType)
        {
        }

     
    }
}