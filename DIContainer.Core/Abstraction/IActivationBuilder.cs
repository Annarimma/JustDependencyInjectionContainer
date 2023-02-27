using System;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Abstraction;

/// <summary>
/// Activation Builder Interface.
/// </summary>
public interface IActivationBuilder
{
    /// <summary>
    /// Return object creation delegate.
    /// </summary>
    /// <param name="descriptor">Service information.</param>
    /// <returns>Delegate.</returns>
    Func<IScope, object> BuildActivation(ServiceMetaInfo descriptor);
}