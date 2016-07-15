using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.MetaData
{
    internal class DynamicControllerMetaData
    {
        public string ControllerName { get; }
        public Type ServiceType { get; }
        public Type ImplementationType { get; }

        public DynamicControllerMetaData(string controllerName, Type serviceType, Type implementationType)
        {
            if (string.IsNullOrEmpty(controllerName)) throw new ArgumentNullException(nameof(controllerName));
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            if (implementationType == null) throw new ArgumentNullException(nameof(implementationType));
            if (!serviceType.GetTypeInfo().IsAssignableFrom(implementationType.GetTypeInfo()))
                throw new ArgumentException($"{implementationType.FullName}不是{serviceType.FullName}的实现类型");
            ControllerName = controllerName;
            ServiceType = serviceType;
            ImplementationType = implementationType;
        }
        public virtual DynamicAttributeMetaData[] GetAttributes()
        {
            return null;
        }
        
        public virtual DynamicActionMetaData[] GetActions()
        {
            return null;
        }
    }
}
