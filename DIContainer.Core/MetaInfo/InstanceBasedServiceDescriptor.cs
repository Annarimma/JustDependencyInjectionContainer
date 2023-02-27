using System;
using DIContainer.Core.Enums;

namespace DIContainer.Core.MetaInfo;

/// <summary>
/// A singleton instance (static type object).
/// </summary>
public class InstanceBasedServiceDescriptor : ServiceMetaInfo
{
    /// <summary>
    /// Gets object instance.
    /// </summary>
    public object Instance { get; init; }

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
}