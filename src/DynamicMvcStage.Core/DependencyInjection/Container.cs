using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace DynamicMvcStage.Core.DependencyInjection
{
    public sealed class Container
    {
        private readonly ConcurrentDictionary<Type , Type> container = new ConcurrentDictionary<Type , Type>();

        public void Register(Type serviceType , Type implementationType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));

            if (implementationType == null) throw new ArgumentNullException(nameof(implementationType));

            if (implementationType.GetTypeInfo().IsAbstract || implementationType.GetTypeInfo().IsInterface)
                throw new ArgumentException($"实现类型{implementationType.FullName}不能为抽象类或接口");

            if (!serviceType.GetTypeInfo().IsAssignableFrom(implementationType.GetTypeInfo()))
                throw new ArgumentException($"类型{implementationType.FullName}未实现或继承{serviceType.FullName}");

            container[serviceType] = implementationType;
        }

        public object Resolve(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            Type implementationType;

            if (!container.TryGetValue(serviceType , out implementationType))
                throw new ArgumentException($"类型{serviceType.FullName}未注册");

            return CreateService(implementationType);
        }

        public object ResolveOrDefault(Type serviceType)
        {
            if (serviceType == null)
                return null;

            Type implementationType;

            if (!container.TryGetValue(serviceType , out implementationType))
                return null;

            return CreateService(implementationType);

        }

        private object CreateService(Type implementationType)
        {
            var constructor = implementationType.GetTypeInfo().DeclaredConstructors.First();
            var parameters = constructor.GetParameters().Select(c => Resolve(c.ParameterType)).ToArray();
            return constructor.Invoke(parameters);
        }
    }
}
