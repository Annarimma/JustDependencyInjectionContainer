using System;
using System.Linq;
using System.Reflection;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Cache;
using DIContainer.Core.ErrorHandler;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders;

/// <summary>
/// Abstract class with base build realization.
/// </summary>
public abstract class BaseActivationBuilder : IActivationBuilder
{
    /// <summary>
    /// Base build activation.
    /// </summary>
    /// <param name="descriptor"><see cref="ServiceMetaInfo"/> describes information about services.</param>
    /// <returns>Creation Delegate.</returns>
    public Func<IScope, object> BuildActivation(ServiceMetaInfo descriptor)
    {
        var serviceMetaInfo = descriptor;
        var implementationType = GetImplementationType(serviceMetaInfo);
        var ctor = GetConstructorInfo(implementationType);
        if (ctor == null)
            return s => null;
        var args = ctor.GetParameters();
        return BuildActivationInternal((TypeBasedServiceDescriptor)serviceMetaInfo, ctor, args);
    }

    /// <summary>
    /// Inheritors implements this method.
    /// </summary>
    /// <param name="typeDescriptor">Type based descriptor.</param>
    /// <param name="ctor">Constructor info.</param>
    /// <param name="args">Constructor parameters.</param>
    /// <returns>Delegate.</returns>
    protected abstract Func<IScope, object> BuildActivationInternal(
        TypeBasedServiceDescriptor typeDescriptor,
        ConstructorInfo ctor,
        ParameterInfo[] args);

    /// <summary>
    /// Return implementation type by descriptor.
    /// </summary>
    /// <param name="descriptor">Descriptor.</param>
    /// <returns>Implementation type.</returns>
    /// <exception cref="InjectionException">Then Interface or Abstract Class can't be instantiated.</exception>
    protected Type GetImplementationType(ServiceMetaInfo descriptor)
    {
        var typeDescriptor = (TypeBasedServiceDescriptor)descriptor;
        var instanceType = typeDescriptor.InstanceType;

        if (instanceType.IsAbstract || instanceType.IsInterface)
        {
            throw new InjectionException(InjectionException.CANNOT_INSTANTIATE_INTERFACE);
        }

        return instanceType;
    }

    protected ConstructorInfo GetConstructorInfo(Type implementationType)
    {
        var constructorInfo = CachedConstructors.GetOrAddConstructor(implementationType);
        return constructorInfo;
    }
}