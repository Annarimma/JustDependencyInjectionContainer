using DIContainer.Core.Abstraction;

namespace DIContainer.Core.Extensions;

/// <summary>
/// Scope Extensions.
/// </summary>
public static class ScopeExtensions
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
}