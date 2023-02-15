using DIContainer.Core.Implementation;
using DIContainer.Sample.Abstractions;
using DIContainer.Sample.Models;

// IService service = new Service();
// var controller = new Controller(service);
// controller.Read();

var builder = new ContainerBuilder();
var container = builder
    .AddSingleton<IRepository, Repository>()
    .AddSingleton<IService, Service>()
    .AddSingleton<Controller>()
    .Build();

var service = container.GetInstance<IService>();
Console.WriteLine(service.ToString());
    
