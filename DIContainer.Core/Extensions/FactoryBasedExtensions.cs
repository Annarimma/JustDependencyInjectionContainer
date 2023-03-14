using System;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Enums;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Extensions;

/// <summary>
/// Factory Based Service Descriptor Extensions.
/// </summary>
public static class FactoryBasedExtensions
{
	/// <summary>
	/// Singleton registration.
	/// </summary>
	/// <param name="builder">Container builder.</param>
	/// <param name="interface">Type of Interface.</param>
	/// <param name="factory">The delegate to register.</param>
	/// <returns><see cref="IContainerBuilder"/>Container Builder.</returns>
	public static IContainerBuilder AddSingleton(
		this IContainerBuilder builder,
		Type @interface,
		Func<IScope, object> factory)
	{
		if (builder == null)
			throw new ArgumentNullException(nameof(builder));

		if (factory == null)
			throw new ArgumentNullException(nameof(factory));

		if (@interface == null)
			throw new ArgumentNullException(nameof(@interface));

		return builder.RegisterFactory(@interface, factory, LifeTime.Singleton);
	}

	/// <summary>
	/// Singleton registration.
	/// </summary>
	/// <param name="builder">Container builder.</param>
	/// <param name="factory">The delegate to register.</param>
	/// <typeparam name="TInterface">Interface type.</typeparam>
	/// <returns>Container Builder.</returns>
	public static IContainerBuilder AddSingleton<TInterface>(
		this IContainerBuilder builder,
		Func<IScope, TInterface> factory)
		where TInterface : class
	{
		if (builder == null)
			throw new ArgumentNullException(nameof(builder));

		if (factory == null)
			throw new ArgumentNullException(nameof(factory));

		return builder.RegisterFactory(typeof(TInterface), factory, LifeTime.Singleton);
	}

	/// <summary>
	/// Transient registration.
	/// </summary>
	/// <param name="builder">Container builder.</param>
	/// <param name="interface">Type of Interface.</param>
	/// <param name="factory">The delegate to register.</param>
	/// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
	public static IContainerBuilder AddTransient(
		this IContainerBuilder builder,
		Type @interface,
		Func<IScope, object> factory)
	{
		if (builder == null)
			throw new ArgumentNullException(nameof(builder));

		if (@interface == null)
			throw new ArgumentNullException(nameof(@interface));

		if (factory == null)
			throw new ArgumentNullException(nameof(factory));

		return builder.RegisterFactory(@interface, factory, LifeTime.Transient);
	}

	/// <summary>
	/// Transient registration.
	/// </summary>
	/// <param name="builder">Container builder.</param>
	/// <param name="factory">The delegate to register.</param>
	/// <typeparam name="TInterface">Type of Interface.</typeparam>
	/// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
	public static IContainerBuilder AddTransient<TInterface>(
		this IContainerBuilder builder,
		Func<IScope, object> factory)
	{
		if (builder == null)
			throw new ArgumentNullException(nameof(builder));

		if (factory == null)
			throw new ArgumentNullException(nameof(factory));

		return builder.RegisterFactory(typeof(TInterface), factory, LifeTime.Transient);
	}

	/// <summary>
	/// Scoped registration.
	/// </summary>
	/// <param name="builder">Container builder.</param>
	/// <param name="interface">Type of Interface.</param>
	/// <param name="factory">The delegate to register.</param>
	/// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
	public static IContainerBuilder AddScoped(
		this IContainerBuilder builder,
		Type @interface,
		Func<IScope, object> factory)
	{
		if (builder == null)
			throw new ArgumentNullException(nameof(builder));

		if (@interface == null)
            throw new ArgumentNullException(nameof(@interface));

		if (factory == null)
			throw new ArgumentNullException(nameof(factory));

		return builder.RegisterFactory(@interface, factory, LifeTime.Scoped);
	}

	/// <summary>
	/// Scoped registration.
	/// </summary>
	/// <param name="builder">Container builder.</param>
	/// <param name="factory">The delegate to register.</param>
	/// <typeparam name="TInterface">Type of Interface.</typeparam>
	/// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
	public static IContainerBuilder AddScoped<TInterface>(
		this IContainerBuilder builder,
		Func<IScope, object> factory)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (factory == null)
            throw new ArgumentNullException(nameof(factory));

        return builder.RegisterFactory(typeof(TInterface), factory, LifeTime.Scoped);
    }

    /// <summary>
    /// General method for Factory Based Service Descriptors.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <param name="interface">Type of Interface.</param>
    /// <param name="factory">The delegate to register.</param>
    /// <param name="lifeTime">Life Time.</param>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    private static IContainerBuilder RegisterFactory(
		this IContainerBuilder builder,
		Type @interface,
		Func<IScope, object> factory,
		LifeTime lifeTime)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (@interface == null)
            throw new ArgumentNullException(nameof(@interface));

        if (factory == null)
            throw new ArgumentNullException(nameof(factory));

        builder.Register(new FactoryBasedServiceDescriptor()
        {
            Factory = factory,
            InterfaceType = @interface,
            LifeTime = lifeTime,
        });

        return builder;
    }
}