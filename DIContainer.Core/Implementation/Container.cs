using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        private sealed class Scope : IScope
        {
            private readonly Container _container;
            private readonly ConcurrentDictionary<Type, object> _scopedInstances = new();    

            public Scope(Container container)
            {
                _container = container;
            }
            
            public object Resolve(Type @interface)
            {
                var descriptor = _container.GetDescriptor(@interface);
                
                if (descriptor.LifeTime == LifeTime.Transient)
                {
                    return _container.GetInstance(@interface, this);
                }
                
                if (descriptor.LifeTime == LifeTime.Scoped || this == _container._rootScope)
                {
                    return _scopedInstances.GetOrAdd(@interface, s => _container.GetInstance(s, this));
                }
                else
                {
                    return _container._rootScope.Resolve(@interface);
                }
            }
        };
        
        private readonly ImmutableDictionary<Type, ServiceMetaInfo> _serviceDescriptors;
        private readonly ConcurrentDictionary<Type, Func<IScope, object>> _builtActivators = new();
        private readonly Scope _rootScope;

        public Container(IEnumerable<ServiceMetaInfo> serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors.ToImmutableDictionary(k => k.InterfaceType);
            _rootScope = new Scope(this);
        }
        
        /// <summary>
        /// Create new scope
        /// </summary>
        /// <returns>New scope</returns>
        public IScope CreateScope()
        {
            return new Scope(this);
        }

        /// <summary>
        /// Get instance
        /// </summary>
        /// <param name="interface">Interface type</param>
        /// <param name="scope">Scope</param>
        /// <returns>Object instance</returns>
        private object GetInstance(Type @interface, IScope scope)
        {
            return _builtActivators.GetOrAdd(@interface, BuildActivation)(scope);
        }
        
        /// <summary>
        /// Return object creation delegate
        /// </summary>
        /// <returns>Delegate</returns>
        private Func<IScope, object> BuildActivation(Type @interface)
        {
            var descriptor = GetDescriptor(@interface);

            switch (descriptor)
            {
                case InstanceBasedServiceDescriptor instanceDescriptor:
                    return _ => instanceDescriptor.Instance;
                case FactoryBasedServiceDescriptor factoryDescriptor:
                    return factoryDescriptor.Factory;
            }
            
            // todo can we use cached args?
            var implementationType = GetImplementationType(descriptor);
            return scope => GetImplementation(scope, implementationType);
        }
        
        /// <summary>
        /// Returns service descriptor of the specified interface
        /// </summary>
        /// <param name="interface">Type of interface</param>
        /// <returns>Descriptor</returns>
        /// <exception cref="ArgumentNullException">When type is null</exception>
        /// <exception cref="InjectionException">When interface doesn't register</exception>
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