using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Abstraction
{
    /// <summary>
    /// Builder keep all dependency
    /// </summary>
    public interface IContainerBuilder
    {
        IContainer Build();

        // todo or just Add ?
        void Register(ServiceMetaInfo descriptor);
    }
}