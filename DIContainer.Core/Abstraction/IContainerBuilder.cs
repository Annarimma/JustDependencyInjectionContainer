using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Abstraction
{
    /// <summary>
    /// Builder keep all dependency.
    /// </summary>
    public interface IContainerBuilder
    {
        /// <summary>
        /// Build container.
        /// </summary>
        /// <returns><see cref="IContainer"/> - Created container.</returns>
        IContainer Build();

        /// <summary>
        /// Register dependency.
        /// </summary>
        /// <param name="descriptor">Service information.</param>
        void Register(ServiceMetaInfo descriptor);
    }
}