using System;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Enums;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Extensions;

/// <summary>
/// Type Based Service Descriptor Extensions
/// </summary>
public static class TypeBased
{
    /// <summary>
    /// Singleton registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <param name="interface">Type of Interface</param>
    /// <param name="implementation">Type of Implementation</param>
    /// <returns>IContainerBuilder</returns>
    public static IContainerBuilder AddSingleton(this IContainerBuilder builder,
        Type @interface, Type implementation)
    {
        return builder.RegisterType(@interface, implementation, LifeTime.Singleton);
    }
    
    /// <summary>
    /// Singleton registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <param name="implementation">Type of Implementation</param>
    /// <typeparam name="TInterface">Type of Interface</typeparam>
    /// <returns>IContainerBuilder</returns>
    public static IContainerBuilder AddSingleton<TInterface>(this IContainerBuilder builder, Type implementation)
    {
        return builder.RegisterType(typeof(TInterface), implementation, LifeTime.Singleton);
    }

    /// <summary>
    /// Singleton registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <typeparam name="TInterface">Type of Interface</typeparam>
    /// <typeparam name="TImplementation">Type of Implementation</typeparam>
    /// <returns>IContainerBuilder</returns>
    public static  IContainerBuilder AddSingleton<TInterface, TImplementation>(this IContainerBuilder builder) 
        where TInterface : class 
        where TImplementation : class, TInterface
    {
        return builder.RegisterType(typeof(TInterface), typeof(TImplementation), LifeTime.Singleton);
    }
    
    /// <summary>
    /// Transient registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <param name="interface">Type of Interface</param>
    /// <param name="implementation">Type of Implementation</param>
    /// <returns>IContainerBuilder</returns>
    public static IContainerBuilder AddTransient(this IContainerBuilder builder,
        Type @interface,
        Type implementation)
    {
        return builder.RegisterType(@interface, implementation, LifeTime.Transient);
    }
    
    /// <summary>
    /// Singleton registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <param name="implementation">Type of Implementation</param>
    /// <typeparam name="TInterface">Type of Interface</typeparam>
    /// <returns>IContainerBuilder</returns>
    public static  IContainerBuilder AddTransient<TInterface>(this IContainerBuilder builder,
        Type implementation) 
        where TInterface : class
    {
        return builder.RegisterType(typeof(TInterface), implementation, LifeTime.Transient);
    }
    
    /// <summary>
    /// Transient registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <typeparam name="TInterface">Type of Interface</typeparam>
    /// <typeparam name="TImplementation">Type of Implementation</typeparam>
    /// <returns>IContainerBuilder</returns>
    public static  IContainerBuilder AddTransient<TInterface, TImplementation>(this IContainerBuilder builder) 
        where TInterface : class 
        where TImplementation : class, TInterface
    {
        return builder.RegisterType(typeof(TInterface), typeof(TImplementation), LifeTime.Transient);
    }

    /// <summary>
    /// Scoped registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <param name="interface">Type of Interface</param>
    /// <param name="implementation">Type of Implementation</param>
    /// <returns>IContainerBuilder</returns>
    public static IContainerBuilder AddScoped(this IContainerBuilder builder,
        Type @interface,
        Type implementation)
    {
        return builder.RegisterType(@interface, implementation, LifeTime.Scoped);
    }
    
    /// <summary>
    /// Scope registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <param name="implementation">Type of Implementation</param>
    /// <typeparam name="TInterface">Type of Interface</typeparam>
    /// <returns>IContainerBuilder</returns>
    public static  IContainerBuilder AddScoped<TInterface>(this IContainerBuilder builder,
        Type implementation) 
        where TInterface : class
    {
        return builder.RegisterType(typeof(TInterface), implementation, LifeTime.Scoped);
    }

    /// <summary>
    /// Scoped registration
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <typeparam name="TInterface">Type of Interface</typeparam>
    /// <typeparam name="TImplementation">Type of Implementation</typeparam>
    /// <returns>IContainerBuilder</returns>
    public static IContainerBuilder AddScoped<TInterface, TImplementation>(this IContainerBuilder builder) 
        where TInterface : class
        where TImplementation : class, TInterface
    {
        return builder.RegisterType(typeof(TInterface), typeof(TImplementation), LifeTime.Scoped);
    }

    /// <summary>
    /// General method for Type Based Service Descriptors
    /// </summary>
    /// <param name="builder">Container builder</param>
    /// <param name="interface">Type of Interface</param>
    /// <param name="implementation">Type of Implementation</param>
    /// <param name="lifeTime">Life Time</param>
    /// <returns>IContainerBuilder</returns>
    private static IContainerBuilder RegisterType(
        this IContainerBuilder builder,
        Type @interface,
        Type implementation,
        LifeTime lifeTime)
    {
        builder.Register(new TypeBasedServiceDescriptor()
        {
            ImplementationType = implementation,
            InterfaceType = @interface,
            LifeTime = lifeTime
        });

        return builder;
    }
}