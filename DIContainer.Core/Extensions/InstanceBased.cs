using System;
using DIContainer.Core.Abstraction;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Extensions;

/// <summary>
/// Instance Based Service Descriptor Extensions
/// </summary>
public static class InstanceBased
{
    /// <summary>
    /// Singleton registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <param name="interface">Type of Interface</param>
    /// <param name="instance">Instance</param>
    /// <returns>IContainerBuilder</returns>
    public static IContainerBuilder AddSingleton(this IContainerBuilder builder,
        Type @interface,
        object instance)
    {
        return builder.RegisterInstance(@interface, instance);
    }
    
    /// <summary>
    /// Singleton registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <param name="instance">Instance</param>
    /// <typeparam name="TInterface">Type of Interface</typeparam>
    /// <returns>IContainerBuilder</returns>
    public static IContainerBuilder AddSingleton<TInterface>(this IContainerBuilder builder,
        object instance)
    {
        return builder.RegisterInstance(typeof(TInterface), instance);
    }

    /// <summary>
    /// General method for Instance Registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <param name="interface">Type of Interface</param>
    /// <param name="instance">Instance</param>
    /// <returns>IContainerBuilder</returns>
    private static IContainerBuilder RegisterInstance(this IContainerBuilder builder, 
        Type @interface, 
        object instance)
    {
        builder
            .Register(new InstanceBasedServiceDescriptor(@interface, instance));
        return builder;
    }
}