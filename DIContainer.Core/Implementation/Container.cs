using System;
using System.Collections.Generic;
using System.Linq;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Cache;
using DIContainer.Core.Enums;
using DIContainer.Core.ErrorHandler;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Implementation
{
    public class Container : IContainer
    {
        private readonly Dictionary<Type, ServiceMetaInfo> _serviceDescriptors;
        
        public Container(Dictionary<Type, ServiceMetaInfo> serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors;
        }
        
        public T GetInstance<T>() where T : class
        {
            return (T)GetInstance(typeof(T));
        }

        private object GetInstance(Type serviceType)
        {
            var descriptor = _serviceDescriptors
                .FirstOrDefault(x => x.Key == serviceType);

            if (descriptor.Key == null)
            {
                throw new InjectionException(String.Format(InjectionException.MISSING_DEPENDENCY, serviceType));
            }
            
            if (descriptor.Value.Instance != null)
            {
                return descriptor.Value.Instance;
            }

            var implementationType = descriptor.Value.ImplementationType ?? descriptor.Key;

            if (implementationType.IsAbstract || implementationType.IsInterface)
            {
                throw new InjectionException(InjectionException.CANNOT_INSTANTIATE_INTERFACE);
            }
            
            var constructorInfo = CachedConstructors.GetConstructor(implementationType);

            var parameters = CachedParameters
                .GetParameters(constructorInfo)
                .Select(x => GetInstance(x.ParameterType))
                .ToArray();

            var implementation = constructorInfo.Invoke(parameters);
            
            if (descriptor.Value.LifeCycle == LifeCycle.Singleton)
            {
                descriptor.Value.Instance = implementation;
            }
            return implementation;
        }
    }
}