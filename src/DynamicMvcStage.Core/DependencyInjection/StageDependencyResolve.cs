using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DynamicMvcStage.Core.DependencyInjection
{
    public class StageDependencyResolve : IDependencyResolver
    {
        private readonly Container container;
        public StageDependencyResolve(Container container)
        {
            this.container = container;
        }
        public object GetService(Type serviceType)
        {
            if (serviceType == null) return null;
            return container.ResolveOrDefault(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return null;
        }
    }
}
