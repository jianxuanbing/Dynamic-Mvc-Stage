using DynamicMvcStage.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core
{
    public sealed class DynamicControllerContext
    {
        public string ControllerName { get; set; }

        public Type ControllerType { get; set; }

        public DynamicControllerContext(string controllerName)
        {
            ControllerName = DynamicControllerHelper.GetDynamicControllerName(controllerName);
        }
    }
}
