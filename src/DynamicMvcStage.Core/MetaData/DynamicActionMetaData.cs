using DynamicMvcStage.Core.Extensions;
using DynamicMvcStage.Core.MetaData.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.MetaData
{
    public class DynamicActionMetaData
    {
        public string ActionName { get;  }
        public MethodInfo Method { get;  }
        public object State { get; set; }

        public DynamicActionMetaData(string actionName,MethodInfo method)
        {
            ActionName = actionName;
            Method = method;
        }

        public virtual DynamicActionResultMetaData GetActionResult()
        {
            ViewAttribute viewAttribute = Method.GetCustomAttribute<ViewAttribute>();
            if (viewAttribute == null) throw new ArgumentException(nameof(viewAttribute));
            return new DynamicViewResultMetaData(string.IsNullOrEmpty(viewAttribute.Name) ? Method.Name : viewAttribute.Name , viewAttribute.Master) { Method = Method };
        }

        public virtual DynamicAttributeMetaData[] GetDynamicAttributes()
        {
            IEnumerable<CustomAttributeData> customAttributeDatas = Method.CustomAttributes.
                FilterType<CustomAttributeData , ActionAttribute>(c => c.AttributeType).
                FilterType<CustomAttributeData , ActionResultAttribute>(c => c.AttributeType);
            return customAttributeDatas.Select(c => new DynamicAttributeMetaData(c)).ToArray();
        }
    }
}
