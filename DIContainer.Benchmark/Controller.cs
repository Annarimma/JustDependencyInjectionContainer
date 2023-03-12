namespace DIContainer.Benchmark;

public class Controller
{
    private readonly IService _service;

    public Controller(IService service)
    {
        _service = service;
    }
}