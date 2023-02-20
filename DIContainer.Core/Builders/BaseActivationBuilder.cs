using System;
using System.Linq;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Cache;
using DIContainer.Core.ErrorHandler;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders;

public abstract class BaseActivationBuilder
{
    /// <summary>
    /// Return implementation type by descriptor
    /// </summary>
    /// <param name="descriptor">Descriptor</param>
    /// <returns>Implementation type</returns>
    /// <exception cref="InjectionException">Then Interface or Abstract Class can't be instantiated</exception>
    protected Type GetImplementationType(ServiceMetaInfo descriptor)
    {
        var typeDescriptor = (TypeBasedServiceDescriptor)descriptor;
        var implementationType = typeDescriptor.ImplementationType;

        if (implementationType.IsAbstract || implementationType.IsInterface)
        {
            throw new InjectionException(InjectionException.CANNOT_INSTANTIATE_INTERFACE);
        }

        return implementationType;
    }
    
    /// <summary>
    /// Return object instance of requested Implementation Type
    /// </summary>
    /// <param name="scope">Scope</param>
    /// <param name="implementationType">Implementation Type</param>
    /// <returns>Object instance</returns>
    /// <exception cref="ArgumentNullException"></exception>
    protected object GetImplementation(IScope scope, Type implementationType)
    {
        if (scope == null) throw new ArgumentNullException(nameof(scope));
        
        var constructorInfo = CachedConstructors.GetConstructor(implementationType);

        var parameters = CachedParameters
            .GetParameters(constructorInfo)
            .Select(x => scope.Resolve(x.ParameterType))
            .ToArray();

        var implementation = constructorInfo.Invoke(parameters);
        return implementation;
    }
}