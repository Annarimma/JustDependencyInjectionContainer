using System;
using System.Linq;
using System.Reflection;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Cache;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders;

/// <summary>
/// Reflection Activator creates a new instance of a type
/// using reflection to select and invoke a constructor
/// based on the available service registrations.
/// </summary>
public class ReflectionActivationBuilder : BaseActivationBuilder
{
    protected override Func<IScope, object> BuildActivationInternal(
        TypeBasedServiceDescriptor typeDescriptor,
        ConstructorInfo ctor,
        ParameterInfo[] args)
    {
        if (typeDescriptor == null)
            throw new ArgumentNullException(nameof(typeDescriptor));

        if (ctor == null)
            throw new ArgumentNullException(nameof(ctor));

        if (args == null)
            throw new ArgumentNullException(nameof(args));

        var implementationType = GetImplementationType(typeDescriptor);
        return scope => GetImplementation(scope, implementationType);
    }

    /// <summary>
    /// Return object instance of requested Implementation Type.
    /// </summary>
    /// <param name="scope">Scope.</param>
    /// <param name="implementationType">Implementation Type.</param>
    /// <returns>Object instance.</returns>
    /// <exception cref="ArgumentNullException">When scope is null.</exception>
    private object GetImplementation(IScope scope, Type implementationType)
    {
        if (scope == null)
            throw new ArgumentNullException(nameof(scope));

        if (implementationType == null)
            throw new ArgumentNullException(nameof(implementationType));

        var constructorInfo = GetConstructorInfo(implementationType);

        var parameters = CachedParameters
            .GetParameters(constructorInfo)
            .Select(x => scope.Resolve(x.ParameterType))
            .ToArray();

        return constructorInfo.Invoke(parameters);
    }
}