using System;

namespace DIContainer.Core.Abstraction;

public interface IScope: IDisposable, IAsyncDisposable
{
    public object Resolve(Type @interface);
    
    /// <summary>
    /// Determine whether or not a service has been registered.
    /// </summary>
    /// <typeparam name="TInterface">The service to test for the registration of.</typeparam>
    /// <returns>True if the service is registered.</returns>
    bool IsRegistered<TInterface>();
}