using System;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Enums;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Extensions;

/// <summary>
/// Factory Based Service Descriptor Extensions.
/// </summary>
public static class FactoryBased
{
    /// <summary>
    /// Singleton registration.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <param name="interface">Type of Interface.</param>
    /// <param name="factory">Factory.</param>
    /// <returns><see cref="IContainerBuilder"/>Container Builder.</returns>
    public static IContainerBuilder AddSingleton(
        this IContainerBuilder builder,
        Type @interface,
        Func<IScope, object> factory)
    {
        return builder.RegisterFactory(@interface, factory, LifeTime.Singleton);
    }

    /// <summary>
    /// Transient registration.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <param name="interface">Type of Interface.</param>
    /// <param name="factory">Factory.</param>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    public static IContainerBuilder AddTransient(
        this IContainerBuilder builder,
        Type @interface,
        Func<IScope, object> factory)
    {
        return builder.RegisterFactory(@interface, factory, LifeTime.Transient);
    }

    /// <summary>
    /// Scoped registration.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <param name="interface">Type of Interface.</param>
    /// <param name="factory">Factory.</param>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    public static IContainerBuilder AddScoped(
        this IContainerBuilder builder,
        Type @interface,
        Func<IScope, object> factory)
    {
        return builder.RegisterFactory(@interface, factory, LifeTime.Scoped);
    }

    /// <summary>
    /// General method for Factory Based Service Descriptors.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <param name="interface">Type of Interface.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="lifeTime">Life Time.</param>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    private static IContainerBuilder RegisterFactory(
        this IContainerBuilder builder,
        Type @interface,
        Func<IScope, object> factory,
        LifeTime lifeTime)
    {
        builder.Register(new FactoryBasedServiceDescriptor()
        {
            Factory = factory,
            InterfaceType = @interface,
            LifeTime = lifeTime,
        });

        return builder;
    }
}