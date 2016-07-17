using DynamicMvcStage.Core.Controllers;
using DynamicMvcStage.Core.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.Controllers
{
    public sealed class DynamicControllerContext
    {
        public string ControllerName { get; set; }

        public Type ControllerType { get; set; }

        public DynamicControllerMetaData DynamicControllerMetaData { get; set; }

        public DynamicControllerContext(string controllerName)
        {
            ControllerName = DynamicMvcHelper.GetDynamicControllerName(controllerName);
        }
    }
}
