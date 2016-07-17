using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DynamicMvcStage.Core.Controllers.Builders
{
    public interface IDynamicControllerTypeBuilder
    {
        Type BuildControllerType(DynamicControllerContext context);
    }
}
