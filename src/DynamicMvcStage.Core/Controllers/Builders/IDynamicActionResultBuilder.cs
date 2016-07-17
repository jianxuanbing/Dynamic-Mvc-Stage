using DynamicMvcStage.Core.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.Controllers.Builders
{
    public interface IDynamicActionResultBuilder
    {
        object BuildActionResult(DynamicActionResultMetaData dynamicActionResultMetaData);
    }
}
