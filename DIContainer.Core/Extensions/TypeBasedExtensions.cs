using System;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Enums;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Extensions;

/// <summary>
/// Type Based Service Descriptor Extensions.
/// </summary>
public static class TypeBasedExtensions
{
	/// <summary>
	/// Singleton registration.
	/// </summary>
	/// <param name="builder">Container builder.</param>
	/// <param name="interface">Type of Interface.</param>
	/// <param name="implementation">Type of Implementation.</param>
	/// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
	public static IContainerBuilder AddSingleton(
		this IContainerBuilder builder,
		Type @interface,
		Type implementation)
	{
		if (builder == null)
			throw new ArgumentNullException(nameof(builder));

		if (@interface == null)
			throw new ArgumentNullException(nameof(@interface));

		if (implementation == null)
			throw new ArgumentNullException(nameof(implementation));

		return builder.RegisterType(@interface, implementation, LifeTime.Singleton);
	}

	/// <summary>
	/// Singleton registration.
	/// </summary>
	/// <param name="builder">Container builder.</param>
	/// <param name="implementation">Type of Implementation.</param>
	/// <typeparam name="TInterface">Type of Interface.</typeparam>
	/// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
	public static IContainerBuilder AddSingleton<TInterface>(
		this IContainerBuilder builder,
		Type implementation)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (implementation == null)
            throw new ArgumentNullException(nameof(implementation));

        return builder.RegisterType(typeof(TInterface), implementation, LifeTime.Singleton);
    }

    /// <summary>
    /// Singleton registration.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <typeparam name="TInterface">Type of Interface.</typeparam>
    /// <typeparam name="TImplementation">Type of Implementation.</typeparam>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    public static IContainerBuilder AddSingleton<TInterface, TImplementation>(this IContainerBuilder builder)
		where TInterface : class
		where TImplementation : class, TInterface
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        return builder.RegisterType(typeof(TInterface), typeof(TImplementation), LifeTime.Singleton);
    }

    /// <summary>
    /// Transient registration.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <param name="interface">Type of Interface.</param>
    /// <param name="implementation">Type of Implementation.</param>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    public static IContainerBuilder AddTransient(
		this IContainerBuilder builder,
		Type @interface,
		Type implementation)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (@interface == null)
            throw new ArgumentNullException(nameof(@interface));

        if (implementation == null)
            throw new ArgumentNullException(nameof(implementation));

        return builder.RegisterType(@interface, implementation, LifeTime.Transient);
    }

    /// <summary>
    /// Singleton registration.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <param name="implementation">Type of Implementation.</param>
    /// <typeparam name="TInterface">Type of Interface.</typeparam>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    public static IContainerBuilder AddTransient<TInterface>(
		this IContainerBuilder builder,
		Type implementation)
		where TInterface : class
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (implementation == null)
            throw new ArgumentNullException(nameof(implementation));

        return builder.RegisterType(typeof(TInterface), implementation, LifeTime.Transient);
    }

    /// <summary>
    /// Transient registration.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <typeparam name="TInterface">Type of Interface.</typeparam>
    /// <typeparam name="TImplementation">Type of Implementation.</typeparam>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    public static IContainerBuilder AddTransient<TInterface, TImplementation>(this IContainerBuilder builder)
		where TInterface : class
		where TImplementation : class, TInterface
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        return builder.RegisterType(typeof(TInterface), typeof(TImplementation), LifeTime.Transient);
    }

    /// <summary>
    /// Scoped registration.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <param name="interface">Type of Interface.</param>
    /// <param name="implementation">Type of Implementation.</param>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    public static IContainerBuilder AddScoped(
		this IContainerBuilder builder,
		Type @interface,
		Type implementation)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (@interface == null)
            throw new ArgumentNullException(nameof(@interface));

        if (implementation == null)
            throw new ArgumentNullException(nameof(implementation));

        return builder.RegisterType(@interface, implementation, LifeTime.Scoped);
    }

    /// <summary>
    /// Scope registration.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <param name="implementation">Type of Implementation.</param>
    /// <typeparam name="TInterface">Type of Interface.</typeparam>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    public static IContainerBuilder AddScoped<TInterface>(
		this IContainerBuilder builder,
		Type implementation)
		where TInterface : class
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (implementation == null)
            throw new ArgumentNullException(nameof(implementation));

        return builder.RegisterType(typeof(TInterface), implementation, LifeTime.Scoped);
    }

    /// <summary>
    /// Scoped registration.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <typeparam name="TInterface">Type of Interface.</typeparam>
    /// <typeparam name="TImplementation">Type of Implementation.</typeparam>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    public static IContainerBuilder AddScoped<TInterface, TImplementation>(this IContainerBuilder builder)
		where TInterface : class
		where TImplementation : class, TInterface
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        return builder.RegisterType(typeof(TInterface), typeof(TImplementation), LifeTime.Scoped);
    }

    /// <summary>
    /// General method for Type Based Service Descriptors.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <param name="interface">Type of Interface.</param>
    /// <param name="instanceType">Type of Instance.</param>
    /// <param name="lifeTime">Life Time: singleton, transient or scoped.</param>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    private static IContainerBuilder RegisterType(
		this IContainerBuilder builder,
		Type @interface,
		Type instanceType,
		LifeTime lifeTime)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (@interface == null)
            throw new ArgumentNullException(nameof(@interface));

        if (instanceType == null)
            throw new ArgumentNullException(nameof(instanceType));

        builder.Register(new TypeBasedServiceDescriptor(@interface)
        {
            InstanceType = instanceType,
            InterfaceType = @interface,
            LifeTime = lifeTime,
        });

        return builder;
    }
}