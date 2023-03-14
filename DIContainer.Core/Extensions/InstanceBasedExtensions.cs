using System;
using DIContainer.Core.Abstraction;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Extensions;

/// <summary>
/// Instance Based Service Descriptor Extensions.
/// </summary>
public static class InstanceBasedExtensions
{
	// todo: AddInstance(new Instance);

	/// <summary>
	/// Singleton registration.
	/// </summary>
	/// <param name="builder">Container builder.</param>
	/// <param name="interface">Type of Interface.</param>
	/// <param name="instance">Instance.</param>
	/// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
	public static IContainerBuilder AddSingleton(
		this IContainerBuilder builder,
		Type @interface,
		object instance)
	{
		if (builder == null)
			throw new ArgumentNullException(nameof(builder));

		if (@interface == null)
			throw new ArgumentNullException(nameof(@interface));

		if (instance == null)
			throw new ArgumentNullException(nameof(instance));

		return builder.RegisterInstance(@interface, instance);
	}

	/// <summary>
	/// Singleton registration.
	/// </summary>
	/// <param name="builder">Container builder.</param>
	/// <param name="instance">Instance.</param>
	/// <typeparam name="TInterface">Type of Interface.</typeparam>
	/// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
	public static IContainerBuilder AddSingleton<TInterface>(
		this IContainerBuilder builder,
		object instance)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (instance == null)
            throw new ArgumentNullException(nameof(instance));

        return builder.RegisterInstance(typeof(TInterface), instance);
    }

    /// <summary>
    /// General method for Instance Registration.
    /// </summary>
    /// <param name="builder">Container builder.</param>
    /// <param name="interface">Type of Interface.</param>
    /// <param name="instance">Instance.</param>
    /// <returns><see cref="IContainerBuilder"/> - Container Builder.</returns>
    private static IContainerBuilder RegisterInstance(
		this IContainerBuilder builder,
		Type @interface,
		object instance)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (@interface == null)
            throw new ArgumentNullException(nameof(@interface));

        if (instance == null)
            throw new ArgumentNullException(nameof(instance));

        return builder.Register(new InstanceBasedServiceDescriptor(@interface, instance));
    }
}