namespace DIContainer.Core.Abstraction;

public interface IScope
{
    T Resolve<T>() where T : class;
}