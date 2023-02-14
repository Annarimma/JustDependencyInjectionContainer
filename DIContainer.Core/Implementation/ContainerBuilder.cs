using System.Collections.Generic;
using DIContainer.Core.Abstraction;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Implementation
{
    /// <summary>
    /// Builder keep all dependency
    /// </summary>
    public class ContainerBuilder : IContainerBuilder
    {
        private readonly List<ServiceMetaInfo> _serviceDescriptors = new ();

        // like ConfigureServices
        public IContainer Build()
        {
            return new Container(_serviceDescriptors);
        }

        public void Register(ServiceMetaInfo descriptor)
        {
            _serviceDescriptors.Add(descriptor);
        }
    }
}