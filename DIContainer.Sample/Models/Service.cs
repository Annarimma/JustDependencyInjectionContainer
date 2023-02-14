using DIContainer.Sample.Abstractions;

namespace DIContainer.Sample.Models;

public class Service : IService
{
    private readonly IRepository _repository;
    
    public Service(IRepository repository)
    {
        _repository = repository;
    }

    public Service()
    {
        
    }
}