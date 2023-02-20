using System.Collections.Generic;
using DIContainer.Core.Abstraction;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders
{
    /// <summary>
    /// Builder keep all dependency
    /// </summary>
    public class ContainerBuilder : IContainerBuilder
    {
        private readonly List<ServiceMetaInfo> _serviceDescriptors = new ();
        private readonly IActivationBuilder _builder;

        public ContainerBuilder(IActivationBuilder builder)
        {
            _builder = builder;
        }

        // like ConfigureServices
        public IContainer Build()
        {
            return new Container.Container(_serviceDescriptors, _builder);
        }

        public void Register(ServiceMetaInfo descriptor)
        {
            _serviceDescriptors.Add(descriptor);
        }
    }
}