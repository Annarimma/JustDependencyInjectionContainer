using System;

namespace DIContainer.Core.MetaInfo;

/// <summary>
/// Service Implementation Class.
/// </summary>
public class TypeBasedServiceDescriptor : ServiceMetaInfo
{
    /// <summary>
    /// Gets implementation type.
    /// </summary>
    public Type ImplementationType { get; init; }
}