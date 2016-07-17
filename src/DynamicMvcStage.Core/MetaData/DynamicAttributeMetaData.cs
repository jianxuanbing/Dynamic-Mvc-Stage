using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.MetaData
{
    public class DynamicAttributeMetaData
    {
        public CustomAttributeData CustomAttributeData { get; set; }

        public DynamicAttributeMetaData(CustomAttributeData customAttributeData)
        {
            this.CustomAttributeData = customAttributeData;
        }
    }
}
