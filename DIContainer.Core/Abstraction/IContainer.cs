namespace DIContainer.Core.Abstraction
{
    public interface IContainer
    {
        T GetInstance<T>() where T : class;
    }
}