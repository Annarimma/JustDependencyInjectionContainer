using System;

namespace DIContainer.Core.MetaInfo;

/// <summary>
/// Identifies a service according to a type to which it can be assigned.
/// </summary>
public sealed class TypeBasedServiceDescriptor : ServiceMetaInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TypeBasedServiceDescriptor"/> class.
    /// </summary>
    /// <param name="serviceType">Type of the service.</param>
    public TypeBasedServiceDescriptor(Type serviceType)
    {
        InterfaceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
    }

    /// <summary>
    /// Gets instance type.
    /// </summary>
    public Type InstanceType { get; init; }

    /// <summary>
    /// Gets a human-readable description of the service.
    /// </summary>
    /// <value>The description.</value>
    protected override string Description => InterfaceType.FullName!;
}