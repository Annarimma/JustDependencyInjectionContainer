using System;
using DIContainer.Core.Enums;

namespace DIContainer.Core.MetaInfo;

/// <summary>
/// A singleton instance (static type object)
/// </summary>
public class InstanceBasedServiceDescriptor : ServiceMetaInfo
{
    public object Instance { get; init; }

    public InstanceBasedServiceDescriptor(Type interfaceType, object instance)
    {
        LifeTime = LifeTime.Singleton;
        InterfaceType = interfaceType;
        Instance = instance;
    }
}