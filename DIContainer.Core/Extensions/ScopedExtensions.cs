using DIContainer.Core.Abstraction;

namespace DIContainer.Core.Extensions;

/// <summary>
/// Scope Extensions.
/// </summary>
public static class ScopedExtensions
{
    /// <summary>
    /// Generic extension of Resolve method.
    /// </summary>
    /// <param name="scope">Scope.</param>
    /// <typeparam name="T">Type.</typeparam>
    /// <returns>Instance.</returns>
    public static T Resolve<T>(this IScope scope)
        where T : class
        => (T)scope.Resolve(typeof(T));

    /// <summary>
    /// Generic extension of IsRegistered method.
    /// </summary>
    /// <param name="scope">Scope.</param>
    /// <typeparam name="TInterface">Type.</typeparam>
    /// <returns>True, if type is registered.</returns>
    public static bool IsRegistered<TInterface>(this IScope scope)
    {
        if (scope == null)
            throw new System.ArgumentNullException(nameof(scope));

        return scope.IsRegistered(typeof(TInterface));
    }
}