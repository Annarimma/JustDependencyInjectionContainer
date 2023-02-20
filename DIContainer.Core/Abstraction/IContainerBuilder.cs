using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Abstraction
{
    /// <summary>
    /// Builder keep all dependency
    /// </summary>
    public interface IContainerBuilder
    {
        IContainer Build();

        void Register(ServiceMetaInfo descriptor);
    }
}