using System;

namespace DIContainer.Core.MetaInfo;

/// <summary>
/// Service Implementation Class
/// </summary>
public class TypeBasedServiceDescriptor : ServiceMetaInfo
{
    public Type ImplementationType { get; init; }
}