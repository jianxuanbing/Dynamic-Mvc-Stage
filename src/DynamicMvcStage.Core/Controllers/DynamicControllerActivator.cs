using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace DynamicMvcStage.Core.Controllers
{
    public class DynamicControllerActivator : IControllerActivator
    {
        private Func<IDependencyResolver> _resolverThunk;

        public DynamicControllerActivator()
        {
            _resolverThunk = () => DependencyResolver.Current;
        }

        public IController Create(RequestContext requestContext , Type controllerType)
        {
            try
            {
                if (controllerType == null) throw new ArgumentNullException(nameof(controllerType));
                ConstructorInfo constructor = controllerType.GetConstructors().FirstOrDefault();
                object[] parameters = constructor.GetParameters().Select(p => _resolverThunk().GetService(p.ParameterType)).ToArray();
                return (ControllerBase)Activator.CreateInstance(controllerType , parameters);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture ,
                        "controller Activator error" ,
                        controllerType) ,
                    ex);
            }
        }
    }
}
