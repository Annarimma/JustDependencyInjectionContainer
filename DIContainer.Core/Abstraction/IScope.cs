using System;

namespace DIContainer.Core.Abstraction;

/// <summary>
/// Scope managing interface.
/// </summary>
public interface IScope : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Create right instance.
    /// </summary>
    /// <param name="interface">Resolving interface type.</param>
    /// <returns>Instance.</returns>
    public object Resolve(Type @interface);

    /// <summary>
    /// Determine whether or not a service has been registered.
    /// </summary>
    /// <typeparam name="TInterface">The service to test for the registration of.</typeparam>
    /// <returns>True if the service is registered.</returns>
    bool IsRegistered<TInterface>();
}