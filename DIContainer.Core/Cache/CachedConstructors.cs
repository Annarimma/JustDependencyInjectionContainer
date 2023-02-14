using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DIContainer.Core.Cache
{
    public static class CachedConstructors
    {
        // todo cuncurrent dictionary
        private static Dictionary<Type, ConstructorInfo> _cachedConstructors 
            = new Dictionary<Type, ConstructorInfo>();
        
        public static ConstructorInfo GetConstructor(Type implementationType)
        {
            if (_cachedConstructors.TryGetValue(implementationType, out var constructor))
            {
                return constructor;
            }
            
            _cachedConstructors[implementationType] = implementationType
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .Single();
            
            constructor = _cachedConstructors[implementationType];

            return constructor;
        }
    }
}