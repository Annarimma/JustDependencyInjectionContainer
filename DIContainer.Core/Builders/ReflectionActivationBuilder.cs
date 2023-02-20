using System;
using DIContainer.Core.Abstraction;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders;

public class ReflectionActivationBuilder : BaseActivationBuilder, IActivationBuilder
{
    public Func<IScope, object> BuildActivation(ServiceMetaInfo descriptor)
    {
        var implementationType = GetImplementationType(descriptor);
        return scope => GetImplementation(scope, implementationType);
    }
}