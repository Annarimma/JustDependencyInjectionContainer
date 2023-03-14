using System;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Abstraction;

/// <summary>
/// Builder keep all dependency.
/// </summary>
public interface IContainerBuilder
{
    /// <summary>
    /// Build container.
    /// </summary>
    /// <returns><see cref="IContainer"/> - Created container.</returns>
    public IContainer Build();

    /// <summary>
    /// Register dependency.
    /// </summary>
    /// <param name="descriptor">Service information.</param>
    public IContainerBuilder Register(ServiceMetaInfo descriptor);

    public IContainerBuilder As(Type @interface);

    public IContainerBuilder Register<T>() where T : class;
}