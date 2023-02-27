using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Cache;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders;

/// <summary>
/// Reflection Activator creates a new instance of a type
/// using reflection to select and invoke a constructor
/// based on the available service registrations.
/// </summary>
public class ReflectionActivationBuilder : BaseActivationBuilder, IActivationBuilder
{
    protected override Func<IScope, object> BuildActivationInternal(
        TypeBasedServiceDescriptor typeDescriptor,
        ConstructorInfo ctor,
        ParameterInfo[] args)
    {
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
        {
            throw new ArgumentNullException(nameof(scope));
        }

        var constructorInfo = GetConstructorInfo(implementationType);

        var parameters = CachedParameters
            .GetParameters(constructorInfo)
            .Select(x => scope.Resolve(x.ParameterType))
            .ToArray();

        var implementation = constructorInfo.Invoke(parameters);
        return implementation;
    }
}