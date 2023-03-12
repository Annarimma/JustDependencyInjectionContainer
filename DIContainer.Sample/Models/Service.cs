using DIContainer.Sample.Abstractions;

namespace DIContainer.Sample.Models;

public class Service : IService
{
    public Service(IRepository repository)
    {
    }
}