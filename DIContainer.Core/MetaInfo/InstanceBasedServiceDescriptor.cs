using System;
using DIContainer.Core.Enums;

namespace DIContainer.Core.MetaInfo;

/// <summary>
/// A singleton instance (static type object).
/// </summary>
public sealed class InstanceBasedServiceDescriptor : ServiceMetaInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstanceBasedServiceDescriptor"/> class.
    /// </summary>
    /// <param name="interfaceType">Interface type.</param>
    /// <param name="instance">Object instance.</param>
    public InstanceBasedServiceDescriptor(Type interfaceType, object instance)
    {
        LifeTime = LifeTime.Singleton;
        InterfaceType = interfaceType;
        Instance = instance;
    }

    /// <summary>
    /// Gets object instance.
    /// </summary>
    public object Instance { get; }

    /// <summary>
    /// Gets a <see cref="string"/> that represents the current <see cref="object"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> that represents the current <see cref="object"/>.
    /// </returns>
    protected override string Description => InterfaceType.FullName!;
}