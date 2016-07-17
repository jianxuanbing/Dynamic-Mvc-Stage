using DynamicMvcStage.Core.Extensions;
using DynamicMvcStage.Core.MetaData.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.MetaData
{
    public class DynamicControllerMetaData
    {
        public string ControllerName { get; }
        public Type ServiceType { get; }
        public Type ImplementationType { get; }

        public DynamicControllerMetaData(string controllerName , Type serviceType , Type implementationType)
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
        public virtual DynamicAttributeMetaData[] GetDynamicAttributes()
        {
            IEnumerable<CustomAttributeData> customAttributeDatas = ServiceType.GetTypeInfo().CustomAttributes.FilterType<CustomAttributeData , ControllerAttribute>(c => c.AttributeType);
            return customAttributeDatas.Select(c => new DynamicAttributeMetaData(c)).ToArray();
        }

        public virtual DynamicActionMetaData[] GetDynamicActions()
        {
            IEnumerable<MethodInfo> actionMethods = ServiceType.GetTypeInfo().GetMethods().Where(m => m.IsDefined(typeof(ActionAttribute)));
            return GetDynamicActions(actionMethods).ToArray();
        }

        private IEnumerable<DynamicActionMetaData> GetDynamicActions(IEnumerable<MethodInfo> actionMethods)
        {
            foreach (MethodInfo method in actionMethods)
            {
                ActionAttribute actionAttribute = method.GetCustomAttribute<ActionAttribute>();
                yield return new DynamicActionMetaData(method: method , actionName: string.IsNullOrEmpty(actionAttribute.Name) ? method.Name : actionAttribute.Name);
            }
        }
    }
}
