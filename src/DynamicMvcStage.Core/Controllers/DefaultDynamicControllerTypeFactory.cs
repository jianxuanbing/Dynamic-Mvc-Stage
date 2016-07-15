using DynamicMvcStage.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DynamicMvcStage.Core
{
    public class DefaultDynamicControllerTypeFactory : IDynamicControllerTypeFactory
    {
        private readonly IContainer container;
        public DefaultDynamicControllerTypeFactory(IContainer container)
        {
            this.container = container;
        }

        public ControllerBase CreateController(DynamicControllerContext context)
        {
            return container.TryResolve(context.ControllerType) as ControllerBase;
        }

        public Type CreateControllerType(DynamicControllerContext context)
        {

            return context.ControllerType;
        }
    }
}
