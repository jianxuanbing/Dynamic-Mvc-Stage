using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Async;

namespace DynamicMvcStage.Core.DependencyInjection
{
    public static class ContainerExtensions
    {
        public static IContainer Register<TService, TImplementation>(this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            container.Register(typeof(TService), typeof(TImplementation));
            return container;
        }
        public static IContainer Register<TService>(this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            container.Register(typeof(TService), typeof(TService));
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
            container.Register<IAsyncActionInvoker, DynamicAsyncControllerActionInvoker>();
            container.Register<IDynamicControllerTypeFactory, DefaultDynamicControllerTypeFactory>();
            return container;
        }
    }
}
