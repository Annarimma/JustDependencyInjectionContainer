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
        // todo Concurrent
        private readonly Dictionary<Type, ServiceMetaInfo> _serviceDescriptors;
        
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

        public Container(IEnumerable<ServiceMetaInfo> serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors.ToDictionary(k => k.InterfaceType);
        }
        
        public IScope CreateScope()
        {
            return new Scope(this);
        }
        
        private T GetInstance<T>(IScope scope) where T : class
        {
            return (T)GetInstance(typeof(T), scope);
        }
        
        /// <summary>
        /// Get instance
        /// </summary>
        /// <param name="interface">Interface type</param>
        /// <param name="scope">Scope</param>
        /// <returns>Object instance</returns>
        private object GetInstance(Type @interface, IScope scope)
        {
            var descriptor = GetDescriptor(@interface);

            switch (descriptor)
            {
                case InstanceBasedServiceDescriptor instanceDescriptor:
                    return instanceDescriptor.Instance;
                case FactoryBasedServiceDescriptor factoryDescriptor:
                    return factoryDescriptor.Factory(scope);
            }

            var implementationType = GetImplementationType(descriptor);
            var implementation = GetImplementation(scope, implementationType);

            return implementation;
        }

        /// <summary>
        /// Returns service descriptor of the specified interface
        /// </summary>
        /// <param name="interface">Type of interface</param>
        /// <returns>Descriptor</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InjectionException"></exception>
        private ServiceMetaInfo GetDescriptor(Type @interface)
        {
            if (@interface == null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }

            if (!_serviceDescriptors.TryGetValue(@interface, out var descriptor))
            {
                throw new InjectionException(string.Format(InjectionException.MISSING_DEPENDENCY, @interface));
            }

            return descriptor;
        }
        
        /// <summary>
        /// Return implementation type by descriptor
        /// </summary>
        /// <param name="descriptor">Descriptor</param>
        /// <returns>Implementation type</returns>
        /// <exception cref="InjectionException">Then Interface or Abstract Class can't be instantiated</exception>
        private static Type GetImplementationType(ServiceMetaInfo descriptor)
        {
            var typeDescriptor = (TypeBasedServiceDescriptor)descriptor;
            var implementationType = typeDescriptor.ImplementationType;

            if (implementationType.IsAbstract || implementationType.IsInterface)
            {
                throw new InjectionException(InjectionException.CANNOT_INSTANTIATE_INTERFACE);
            }

            return implementationType;
        }
        
        /// <summary>
        /// Return object instance of requested Implementation Type
        /// </summary>
        /// <param name="scope">Scope</param>
        /// <param name="implementationType">Implementation Type</param>
        /// <returns>Object instance</returns>
        private object GetImplementation(IScope scope, Type implementationType)
        {
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