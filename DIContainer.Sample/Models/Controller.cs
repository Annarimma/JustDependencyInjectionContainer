using DIContainer.Sample.Abstractions;

namespace DIContainer.Sample.Models;

public class Controller
{
    private readonly IService _service;
    
    public Controller(IService service)
    {
        _service = service;
    }

    public void Read()
    {
       
    }
}