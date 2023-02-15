namespace DIContainer.Core.Abstraction
{
    public interface IContainer
    {
        IScope CreateScope();
    }
}