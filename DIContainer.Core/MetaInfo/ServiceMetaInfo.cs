using System;
using DIContainer.Core.Enums;

namespace DIContainer.Core.MetaInfo
{
    /// <summary>
    /// Base Service descriptor.
    /// </summary>
    public abstract class ServiceMetaInfo
    {
        /// <summary>
        /// Gets interface type.
        /// </summary>
        public Type InterfaceType { get; init; }

        /// <summary>
        /// Gets instance life time.
        /// </summary>
        public LifeTime LifeTime { get; init; }
    }
}