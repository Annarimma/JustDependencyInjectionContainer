using System;
using System.Collections.Generic;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Enums;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Implementation
{
    /// <summary>
    /// Builder keep all dependency
    /// </summary>
    public class ContainerBuilder : IContainerBuilder
    {
        private Dictionary<Type, ServiceMetaInfo> _serviceDescriptors 
            = new Dictionary<Type, ServiceMetaInfo>();
        

        // like ConfigureServices
        public Container Build()
        {
            return new Container(_serviceDescriptors);
        }

        public IContainerBuilder AddSingleton<TInterface>() 
            where TInterface : class
        {
            var interfaceType = typeof(TInterface);
            if (_serviceDescriptors.ContainsKey(interfaceType))
            {
                return this;
            }

            _serviceDescriptors
                .Add(interfaceType, new ServiceMetaInfo()
                {
                    LifeCycle = LifeCycle.Singleton
                });

            return this;
        }

        public IContainerBuilder AddSingleton<TInterface, TImplementation>() 
            where TInterface : class where TImplementation : class, TInterface
        {
            var interfaceType = typeof(TInterface);
            var implementationType = typeof(TImplementation);

            if (_serviceDescriptors.ContainsKey(interfaceType))
            {
                return this;
            }

            _serviceDescriptors
                .Add(interfaceType, new ServiceMetaInfo()
                {
                    ImplementationType = implementationType,
                    LifeCycle = LifeCycle.Singleton
                });

            return this;
        }
        
        public IContainerBuilder AddTransient<TInterface, TImplementation>() 
            where TInterface : class
            where TImplementation : class, TInterface
        {
            var interfaceType = typeof(TInterface);
            var implementationType = typeof(TImplementation);

            if (_serviceDescriptors.ContainsKey(interfaceType))
            {
                return this;
            }
            
            _serviceDescriptors
                .Add(interfaceType, new ServiceMetaInfo()
                {
                    ImplementationType = implementationType,
                    LifeCycle = LifeCycle.Transient
                });

            return this;
        }
    }
}