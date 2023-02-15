using System;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Enums;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Extensions;

public static class Factory
{
    // todo factory methods
    
    /// <summary>
    /// todo comment
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="service"></param>
    /// <param name="factory"></param>
    /// <param name="lifeTime"></param>
    /// <returns></returns>
    private static IContainerBuilder AddFactory(
        this IContainerBuilder builder,
        System.Type service,
        Func<IScope, object> factory,
        LifeTime lifeTime)
    {
        builder.Register(new FactoryBasedServiceDescriptor()
        {
            Factory = factory,
            InterfaceType = service,
            LifeTime = lifeTime
        });

        return builder;
    }
}