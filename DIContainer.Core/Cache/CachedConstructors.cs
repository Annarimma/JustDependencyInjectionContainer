using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace DIContainer.Core.Cache;

/// <summary>
/// Reflection optimization class store cached constructors.
/// </summary>
internal static class CachedConstructors
{
    private static readonly ConcurrentDictionary<Type, ConstructorInfo> _cachedConstructors = new();

    /// <summary>
    /// Get a stored contractor info or add a new one.
    /// </summary>
    /// <param name="implementationType">Implementation Type.</param>
    /// <returns>Constructor Info.</returns>
    public static ConstructorInfo GetOrAddConstructor(Type implementationType)
    {
        if (implementationType == null)
            throw new ArgumentNullException(nameof(implementationType));

        if (_cachedConstructors.TryGetValue(implementationType, out var constructor))
        {
            return constructor;
        }

        _cachedConstructors[implementationType] = implementationType
            .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .Single();

        return _cachedConstructors[implementationType];
    }
}