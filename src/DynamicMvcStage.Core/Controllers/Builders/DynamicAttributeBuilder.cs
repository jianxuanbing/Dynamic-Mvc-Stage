using DynamicMvcStage.Core.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.Controllers.Builders
{
    public class DynamicAttributeBuilder : IDynamicAttributeBuilder
    {
        public virtual CustomAttributeBuilder BuildCustomAttributeBuilder(DynamicAttributeMetaData dynamicAttributeMetaData)
        {
            if (dynamicAttributeMetaData == null) throw new ArgumentNullException(nameof(dynamicAttributeMetaData));
            CustomAttributeData customAttributeData = dynamicAttributeMetaData.CustomAttributeData;
            if (customAttributeData.NamedArguments != null)
            {
                return new CustomAttributeBuilder(customAttributeData.Constructor ,
                    customAttributeData.ConstructorArguments.Select(c => c.Value).ToArray() ,
                    customAttributeData.NamedArguments.Where(n => !n.IsField)
                        .Select(n => (PropertyInfo)n.MemberInfo)
                        .ToArray() , customAttributeData.NamedArguments.Where(n => !n.IsField)
                            .Select(n => n.TypedValue.Value)
                            .ToArray() , customAttributeData.NamedArguments.Where(n => n.IsField)
                                .Select(n => (FieldInfo)n.MemberInfo)
                                .ToArray() , customAttributeData.NamedArguments.Where(n => n.IsField)
                                    .Select(n => n.TypedValue.Value)
                                    .ToArray());
            }
            else
            {
                return new CustomAttributeBuilder(customAttributeData.Constructor ,
                    customAttributeData.ConstructorArguments.Select(c => c.Value).ToArray());
            }
        }
    }
}
