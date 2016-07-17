using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DynamicMvcStage.Core.MetaData.Attributes;

namespace DynamicMvcStage.Core.Controllers
{
    internal static class DynamicMvcHelper
    {
        internal static string GetDynamicControllerName(string controllerName)
        {
            if (string.IsNullOrEmpty(controllerName)) throw new ArgumentNullException(nameof(controllerName));
            int hasController = (controllerName = controllerName.ToLower()).LastIndexOf("controller");
            if (hasController != -1) controllerName = controllerName.Remove(hasController);
            return $"dynamic_{controllerName}Controller";
        }

        internal static bool IsDynamicController(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            return Attribute.IsDefined(serviceType , typeof(ControllerAttribute));
        }

        internal static string GetOriginalControllerName(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            ControllerAttribute controller = serviceType.GetCustomAttribute<ControllerAttribute>();
            if (controller != null)
                if (!string.IsNullOrEmpty(controller.Name)) return controller.Name;
            string serviceName = serviceType.Name.ToLower();
            int hasService = serviceName.LastIndexOf("service");
            return hasService != -1 ? serviceName.Remove(hasService) : serviceName;
        }
    }
}
