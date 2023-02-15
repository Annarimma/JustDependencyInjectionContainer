using System;
using System.Collections.Generic;
using System.Linq;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Cache;
using DIContainer.Core.ErrorHandler;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Implementation
{
    public class Container : IContainer
    {
        private class Scope : IScope
        {
            private readonly Container _container;

            public Scope(Container container)
            {
                _container = container;
            }
            
            public T Resolve<T>() where T : class
            {
                return _container.GetInstance<T>(this);
            }
        };
        
        // todo Concurrent
        private readonly Dictionary<Type, ServiceMetaInfo> _serviceDescriptors;
        
        public Container(IEnumerable<ServiceMetaInfo> serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors.ToDictionary(k => k.InterfaceType);
        }
        
        public IScope CreateScope()
        {
            return new Scope(this);
        }
        
        public T GetInstance<T>(IScope scope) where T : class
        {
            return (T)GetInstance(typeof(T), scope);
        }
        
        private object GetInstance(Type serviceType, IScope scope)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            
            var pair = _serviceDescriptors
                .FirstOrDefault(x => x.Key == serviceType);
            
            if (pair.Key == null)
            {
                throw new InjectionException(String.Format(InjectionException.MISSING_DEPENDENCY, serviceType));
            }

            var descriptor = pair.Value;

            switch (descriptor)
            {
                case InstanceBasedServiceDescriptor ib:
                    return ib.Instance;
                case FactoryBasedServiceDescriptor fd:
                    return fd.Factory(scope);
            }

            var tb = (TypeBasedServiceDescriptor)descriptor;
            var implementationType = tb.ImplementationType;
            
            if (implementationType.IsAbstract || implementationType.IsInterface)
            {
                throw new InjectionException(InjectionException.CANNOT_INSTANTIATE_INTERFACE);
            }
            
            var constructorInfo = CachedConstructors.GetConstructor(implementationType);
            
            var parameters = CachedParameters
                .GetParameters(constructorInfo)
                .Select(x => GetInstance(x.ParameterType, scope))
                .ToArray();
            
            var implementation = constructorInfo.Invoke(parameters);

            return implementation;
        }
    }
}