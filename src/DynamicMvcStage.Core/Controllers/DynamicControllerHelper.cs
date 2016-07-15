using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.Controllers
{
    internal static class DynamicControllerHelper
    {
        internal static string GetDynamicControllerName(string controllerName)
        {
            if (string.IsNullOrEmpty(controllerName)) throw new ArgumentNullException(nameof(controllerName));
            controllerName = controllerName.ToLower();
            int hasController = controllerName.LastIndexOf("controller");
            if (hasController != -1) controllerName = controllerName.Remove( hasController);
            return string.Format("dynamic_{0}Controller", controllerName);
        }
    }
}
