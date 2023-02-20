using System;
using System.Reflection;
using DIContainer.Core.Abstraction;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders;

public class ReflectionActivationBuilder : BaseActivationBuilder, IActivationBuilder
{
    protected override Func<IScope, object> BuildActivationInternal(TypeBasedServiceDescriptor typeDescriptor, 
        ConstructorInfo ctor, 
        ParameterInfo[] args)
    {
        var implementationType = GetImplementationType(typeDescriptor);
        return scope => GetImplementation(scope, implementationType);
    }
}