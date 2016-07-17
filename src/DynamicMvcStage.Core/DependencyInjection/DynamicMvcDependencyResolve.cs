using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DynamicMvcStage.Core.DependencyInjection
{
    //public class DynamicMvcDependencyResolve : IDependencyResolver
    //{
    //    private readonly IContainer container;
    //    public DynamicMvcDependencyResolve(IContainer container)
    //    {
    //        this.container = container;
    //    }
    //    public object GetService(Type serviceType)
    //    {
    //        if (serviceType == null) return null;
    //        return container.TryResolve(serviceType);
    //    }

    //    public IEnumerable<object> GetServices(Type serviceType)
    //    {
    //        return new object[] { };
    //    }
    //}
}
