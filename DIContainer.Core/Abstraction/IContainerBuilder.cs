using DIContainer.Core.Implementation;

namespace DIContainer.Core.Abstraction
{
    /// <summary>
    /// Builder keep all dependency
    /// </summary>
    public interface IContainerBuilder
    {
        Container Build();
        
        IContainerBuilder AddSingleton<TInterface>()
            where TInterface : class;
        
        IContainerBuilder AddSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        IContainerBuilder AddTransient<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;
    }
}