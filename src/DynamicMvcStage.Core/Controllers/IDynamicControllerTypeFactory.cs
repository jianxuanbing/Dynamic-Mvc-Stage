using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DynamicMvcStage.Core
{
    public interface IDynamicControllerTypeFactory
    {
        Type CreateControllerType(DynamicControllerContext context);
        ControllerBase CreateController(DynamicControllerContext context);
    }
}
