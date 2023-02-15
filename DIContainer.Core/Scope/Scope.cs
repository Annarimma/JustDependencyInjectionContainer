using System;
using System.Collections.Generic;
using DIContainer.Core.Abstraction;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Scope;

public class Scope : IScope
{
    private readonly Dictionary<Type, ServiceMetaInfo> _serviceDescriptors;

    public Scope(Dictionary<Type, ServiceMetaInfo> serviceDescriptors)
    {
        _serviceDescriptors = serviceDescriptors;
    }
    
    // todo ...
    public T Resolve<T>() where T : class
    {
        throw new NotImplementedException();
    }
}