using System;
using DIContainer.Core.Enums;

namespace DIContainer.Core.MetaInfo
{
    /// <summary>
    /// Service description
    /// </summary>
    public class ServiceMetaInfo
    {
        public Type ImplementationType { get; set; }
        public LifeCycle LifeCycle { get; set; }
        public object Instance { get; set; }
    }
}