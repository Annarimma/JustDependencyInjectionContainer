using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Enums;
using DIContainer.Core.ErrorHandler;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders
{
    /// <summary>
    /// Container class for dependencies managing.
    /// </summary>
    public class Container : IContainer
    {
        #region private sealed class Scope : IScope

        private sealed class Scope : IScope
        {
            private readonly Container _container;
            private readonly ConcurrentDictionary<Type, object> _scopedInstances = new();
            private readonly ConcurrentStack<object> _disposables = new();

            public Scope(Container container)
            {
                _container = container;
            }

            public object Resolve(Type @interface)
            {
                var descriptor = _container.GetDescriptor(@interface);

                if (descriptor.LifeTime == LifeTime.Transient)
                {
                    return CreateDisposableInstance(@interface);
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

            #region IDisposable

            public void Dispose()
            {
                foreach (var item in _disposables)
                {
                    if (item is IDisposable d)
                    {
                        d.Dispose();
                    }
                    else if (item is IAsyncDisposable ad)
                    {
                        // the program may hang.
                        ad.DisposeAsync().GetAwaiter().GetResult();
                    }
                }
            }

            public async ValueTask DisposeAsync()
            {
                foreach (var item in _disposables)
                {
                    if (item is IAsyncDisposable ad)
                    {
                        await ad.DisposeAsync();
                    }
                    else if (item is IDisposable d)
                    {
                        d.Dispose();
                    }
                }
            }

            #endregion

            public bool IsRegistered(Type @interface)
            {
                return _scopedInstances.Any(k => k.Key == @interface);
            }

            private object CreateDisposableInstance(Type @interface)
            {
                var result = _container.GetInstance(@interface, this);
                if (result is IDisposable || result is IAsyncDisposable)
                {
                    _disposables.Push(result);
                }

                return result;
            }
        }

        #endregion
        private readonly ImmutableDictionary<Type, ServiceMetaInfo> _serviceDescriptors;
        private readonly ConcurrentDictionary<Type, Func<IScope, object>> _builtActivators = new();
        private readonly Scope _rootScope;
        private readonly IActivationBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="serviceDescriptors">Service descriptions.</param>
        /// <param name="builder">The way how to build dependencies.</param>
        public Container(IEnumerable<ServiceMetaInfo> serviceDescriptors, IActivationBuilder builder)
        {
            _builder = builder;
            _serviceDescriptors = serviceDescriptors.ToImmutableDictionary(k => k.InterfaceType);
            _rootScope = new Scope(this);
        }

        #region IScope

        /// <summary>
        /// Create new scope.
        /// </summary>
        /// <returns>New scope.</returns>
        public IScope CreateScope()
        {
            return new Scope(this);
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Dispose method for container.
        /// </summary>
        public void Dispose()
        {
            _rootScope.Dispose();
        }

        /// <summary>
        /// DisposeAsync method for container.
        /// </summary>
        public ValueTask DisposeAsync()
        {
            return _rootScope.DisposeAsync();
        }

        #endregion

        /// <summary>
        /// Get instance by interface type and scope.
        /// </summary>
        /// <param name="interface">Interface type.</param>
        /// <param name="scope">Scope.</param>
        /// <returns>Object instance.</returns>
        private object GetInstance(Type @interface, IScope scope)
        {
            return _builtActivators.GetOrAdd(@interface, BuildActivation)(scope);
        }

        /// <summary>
        /// Return object creation delegate.
        /// </summary>
        /// <returns>Delegate.</returns>
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

            return _builder.BuildActivation(descriptor);
        }

        /// <summary>
        /// Returns service descriptor of the specified interface.
        /// </summary>
        /// <param name="interface">Type of interface.</param>
        /// <returns>Descriptor.</returns>
        /// <exception cref="ArgumentNullException">When type is null.</exception>
        /// <exception cref="InjectionException">When interface doesn't register.</exception>
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
    }
}