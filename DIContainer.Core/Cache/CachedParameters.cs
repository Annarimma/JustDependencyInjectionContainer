using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DIContainer.Core.Cache;

/// <summary>
/// Reflection optimization class store cached constructors parameters.
/// </summary>
internal static class CachedParameters
{
    private static readonly ConcurrentDictionary<ConstructorInfo, List<ParameterInfo>> _cachedParameters = new();

    public static IEnumerable<ParameterInfo> GetParameters(ConstructorInfo constructorInfo)
    {
        if (constructorInfo == null)
            throw new System.ArgumentNullException(nameof(constructorInfo));

        if (_cachedParameters
            .TryGetValue(constructorInfo, out var parameterInfo))
        {
            return parameterInfo;
        }

        _cachedParameters[constructorInfo] = constructorInfo
            .GetParameters()
            .ToList();

        return _cachedParameters[constructorInfo];
    }
}