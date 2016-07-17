using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.Controllers
{
    public interface IDynamicControllerContextManager
    {
        DynamicControllerContext GetContext(string controllerName);

        void RegisterContext(string controllerName , DynamicControllerContext context);
    }
}
