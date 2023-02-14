using DIContainer.Core.Abstraction;
using DIContainer.Core.Enums;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Extensions;

public static class Type
{
    /// <summary>
    /// todo comment
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="serviceInterface"></param>
    /// <param name="serviceImplementation"></param>
    /// <returns></returns>
    public static IContainerBuilder AddSingleton(this IContainerBuilder builder,
        System.Type @serviceInterface, System.Type serviceImplementation)
    {
        return builder.AddType(serviceInterface, serviceImplementation, LifeTime.Singleton);
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
        return builder.AddType(typeof(TInterface), typeof(TImplementation), LifeTime.Singleton);
    }
    
    /// <summary>
    /// todo comment
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="serviceInterface"></param>
    /// <param name="implementation"></param>
    /// <returns></returns>
    public static IContainerBuilder AddTransient(this IContainerBuilder builder,
        System.Type serviceInterface,
        System.Type implementation)
    {
        return builder.AddType(serviceInterface, implementation, LifeTime.Transient);
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
        return builder.AddType(typeof(TInterface), typeof(TImplementation), LifeTime.Transient);
    }

    /// <summary>
    /// todo comment
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="service"></param>
    /// <param name="implementation"></param>
    /// <returns></returns>
    public static IContainerBuilder RegisterScoped(this IContainerBuilder builder,
        System.Type service,
        System.Type implementation)
    {
        return builder.AddType(service, implementation, LifeTime.Scoped);
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
        return builder.AddType(typeof(TInterface), typeof(TImplementation), LifeTime.Scoped);
    }
    
    /// <summary>
    /// todo comment
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="service"></param>
    /// <param name="implementation"></param>
    /// <param name="lifeTime"></param>
    /// <returns></returns>
    private static IContainerBuilder AddType(
        this IContainerBuilder builder,
        System.Type service,
        System.Type implementation,
        LifeTime lifeTime)
    {
        builder.Register(new TypeBasedServiceDescriptor()
        {
            ImplementationType = implementation,
            InterfaceType = service,
            LifeTime = lifeTime
        });

        return builder;
    }
}