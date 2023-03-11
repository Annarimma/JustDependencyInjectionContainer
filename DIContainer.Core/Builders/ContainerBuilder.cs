using System.Collections.Generic;
using DIContainer.Core.Abstraction;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders
{
    /// <summary>
    /// Builder keep all dependencies.
    /// </summary>
    public class ContainerBuilder : IContainerBuilder
    {
        private readonly List<ServiceMetaInfo> _serviceDescriptors = new();
        private readonly IActivationBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerBuilder"/> class.
        /// </summary>
        /// <param name="builder">Activation builder.</param>
        public ContainerBuilder(IActivationBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Build a new container.
        /// </summary>
        /// <returns><see cref="IContainer"/>New container.</returns>
        public IContainer Build()
        {
            return new Container(_serviceDescriptors, _builder);
        }

        /// <summary>
        /// Add new descriptor to descriptors list.
        /// </summary>
        /// <param name="descriptor">Service information.</param>
        public void Register(ServiceMetaInfo descriptor)
        {
            _serviceDescriptors.Add(descriptor);
        }
    }
}