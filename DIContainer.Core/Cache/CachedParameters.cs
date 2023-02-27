using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DIContainer.Core.Cache
{
    /// <summary>
    /// Reflection optimization class store cached constructors parameters.
    /// </summary>
    public static class CachedParameters
    {
        private static readonly ConcurrentDictionary<ConstructorInfo, List<ParameterInfo>> _cachedParameters = new();

        public static IEnumerable<ParameterInfo> GetParameters(ConstructorInfo constructorInfo)
        {
            if (_cachedParameters
                .TryGetValue(constructorInfo, out var parameterInfo))
            {
                return parameterInfo;
            }

            _cachedParameters[constructorInfo] = constructorInfo
                .GetParameters()
                .ToList();

            parameterInfo = _cachedParameters[constructorInfo];

            return parameterInfo;
        }
    }
}