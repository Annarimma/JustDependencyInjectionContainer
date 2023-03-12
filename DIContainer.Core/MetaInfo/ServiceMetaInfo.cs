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
        /// Gets or sets interface type.
        /// </summary>
        public Type InterfaceType { get; set; }

        /// <summary>
        /// Gets instance life time.
        /// </summary>
        public LifeTime LifeTime { get; init; }

        /// <summary>
        /// Gets a human-readable description of the service.
        /// </summary>
        /// <value>The description.</value>
        protected abstract string Description { get; }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current <see cref="object"/>.
        /// </returns>
        public override string ToString()
        {
            return Description;
        }
    }
}