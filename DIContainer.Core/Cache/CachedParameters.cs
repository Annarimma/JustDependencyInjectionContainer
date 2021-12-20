using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DIContainer.Core.Cache
{
    public static class CachedParameters
    {
        private static readonly Dictionary<ConstructorInfo, List<ParameterInfo>> _cachedParameters 
            = new Dictionary<ConstructorInfo, List<ParameterInfo>>();
        
        public static List<ParameterInfo> GetParameters(ConstructorInfo constructorInfo)
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