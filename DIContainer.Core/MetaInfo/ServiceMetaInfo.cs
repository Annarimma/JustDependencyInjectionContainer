using System;
using DIContainer.Core.Enums;

namespace DIContainer.Core.MetaInfo
{
    /// <summary>
    /// Base Service descriptor
    /// </summary>
    public abstract class ServiceMetaInfo
    {
        public Type InterfaceType { get; init; }
        public LifeTime LifeTime { get; init; }
    }
}