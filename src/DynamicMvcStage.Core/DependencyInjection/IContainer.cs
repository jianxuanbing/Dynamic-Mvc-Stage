using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.DependencyInjection
{
    public interface IContainer
    {
        void Register(Type serviceType, Type implementationType);
        object Resolve(Type serviceType);
        object TryResolve(Type serviceType);
    }
}
