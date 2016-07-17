using DynamicMvcStage.Core.Controllers;
using DynamicMvcStage.Core.Controllers.Builders;
using DynamicMvcStage.Core.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace DynamicMvcStage.Core.DependencyInjection
{
    public static class ContainerExtensions
    {
        public static IContainer Register<TService, TImplementation>(this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            container.Register(typeof(TService) , typeof(TImplementation));
            return container;
        }
        public static IContainer Register<TService>(this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            container.Register(typeof(TService) , typeof(TService));
            return container;
        }
        public static TService Resolve<TService>(this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return (TService)container.Resolve(typeof(TService));
        }
        public static IContainer RegisterDynamicMvcCore(this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            container.Register<IAsyncActionInvoker , DynamicAsyncControllerActionInvoker>();
            container.Register<IControllerFactory , DynamicControllerFactory>();
            container.Register<IDynamicControllerContextManager , DynamicControllerContextManager>();
            container.Register<IControllerActivator , DynamicControllerActivator>();

            container.Register<IDynamicControllerTypeBuilder , DynamicControllerTypeBuilder>();
            container.Register<IDynamicAttributeBuilder , DynamicAttributeBuilder>();
            container.Register<IDynamicActionMethodBuilder , DynamicActionMethodBuilder>();
            container.Register<IDynamicActionResultBuilder , DynamicViewResultBuilder>();
            return container;
        }

        public static IContainer RegisterDynamicMvcController<TService>(this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            container.RegisterDynamicMvcController<TService , TService>();
            return container;
        }

        public static IContainer RegisterDynamicMvcController<TService, TImplementation>(this IContainer container)
        {
            return RegisterDynamicMvcController(container , typeof(TService) , typeof(TImplementation));
        }

        public static IContainer RegisterDynamicMvcController(this IContainer container , Type serviceType , Type implementationType)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            if (implementationType == null) throw new ArgumentNullException(nameof(implementationType));
            if (DynamicMvcHelper.IsDynamicController(serviceType))
            {
                container.Register(serviceType , implementationType);
                string originalControllerName = DynamicMvcHelper.GetOriginalControllerName(serviceType);
                DynamicControllerContext context = new DynamicControllerContext(originalControllerName);
                DynamicControllerMetaData controllerMeatData = new DynamicControllerMetaData(context.ControllerName , serviceType , implementationType);
                context.DynamicControllerMetaData = controllerMeatData;
                DynamicControllerContextManager.DirectRegisterContext(originalControllerName , context);
            }          
            return container;
        }
    }
}
