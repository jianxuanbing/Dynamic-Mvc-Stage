using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.MetaData
{
    public abstract class DynamicActionResultMetaData
    {
        public object State { get; set; }
        public MethodInfo Method { get; set; }

    }
}
