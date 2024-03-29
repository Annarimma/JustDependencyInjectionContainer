using System;
using DIContainer.Core.Abstraction;

namespace DIContainer.Core.MetaInfo;

/// <summary>
/// Func used as a factory method.
/// </summary>
public sealed class FactoryBasedServiceDescriptor : ServiceMetaInfo
{
    /// <summary>
    /// Gets factory.
    /// </summary>
    public Func<IScope, object> Factory { get; init; }

    protected override string Description => throw new NotImplementedException();
}