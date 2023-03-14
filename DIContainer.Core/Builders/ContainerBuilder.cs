using System;
using System.Collections.Generic;
using System.Linq;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Enums;
using DIContainer.Core.ErrorHandler;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders
{
	/// <summary>
	/// Builder keeps all dependencies.
	/// </summary>
	public sealed class ContainerBuilder : IContainerBuilder
	{
		private readonly List<ServiceMetaInfo> _serviceDescriptors = new();
		private readonly IActivationBuilder _builder;
		private RegistrationData _registrationData;
		private bool _wasBuilt;

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBuilder"/> class.
		/// </summary>
		public ContainerBuilder()
			: this(new ReflectionActivationBuilder())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBuilder"/> class.
		/// </summary>
		/// <param name="builder">Activation builder.</param>
		public ContainerBuilder(IActivationBuilder builder)
		{
			_builder = builder;
		}

		/// <summary>
		/// Gets the services explicitly assigned to the component.
		/// </summary>
		public IEnumerable<ServiceMetaInfo> Services => _serviceDescriptors;

		/// <summary>
		/// Build a new container.
		/// </summary>
		/// <returns><see cref="IContainer"/>New container.</returns>
		public IContainer Build()
		{
			if (_wasBuilt)
			{
				throw new InjectionException(InjectionException.BUILD_SHOULD_BE_CALLED_ONCE);
			}

        	_wasBuilt = true;

			return new Container(_serviceDescriptors, _builder);
		}

		/// <summary>
		/// Register transient (by default) instance to be created through reflection.
		/// </summary>
		/// <typeparam name="T">The type of the component.</typeparam>
		/// <returns>IContainerBuilder.</returns>
		public IContainerBuilder Register<T>()
			where T : class
		{
			var serviceMetaInfo = new TypeBasedServiceDescriptor(typeof(T))
			{
				InstanceType = typeof(T),
				InterfaceType = typeof(T),
				LifeTime = LifeTime.Transient,
			};
			_registrationData = new RegistrationData(serviceMetaInfo);
			return Register(serviceMetaInfo);
		}

		public IContainerBuilder As(Type @interface)
		{
			if (@interface == null)
			{
				throw new ArgumentNullException(nameof(@interface));
			}

			var serviceMetaInfo = _registrationData.RegisterAs(@interface);
			return Register(serviceMetaInfo);
		}

		/// <summary>
		/// Add new descriptor to descriptors list.
		/// </summary>
		/// <param name="descriptor">Service information.</param>
		/// <returns>IContainerBuilder.</returns>
		/// <exception cref="ArgumentNullException">If Service information is null.</exception>
		public IContainerBuilder Register(ServiceMetaInfo descriptor)
		{
			if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));
			_serviceDescriptors.Add(descriptor);
			return this;
		}
	}
}