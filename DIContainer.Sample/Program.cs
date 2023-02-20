using DIContainer.Core.Abstraction;
using DIContainer.Core.Builders;
using DIContainer.Core.Extensions;
using DIContainer.Sample.Abstractions;
using DIContainer.Sample.Models;

IContainerBuilder builder = new ContainerBuilder();
var container = builder
    .AddSingleton<IRepository, Repository>()
    .AddScoped<IService, Service>()
    //.AddSingleton<Controller>()
    .Build();

var concreteInstance = container
    .CreateScope()
    .Resolve<IRepository>();
Console.WriteLine(concreteInstance.ToString());
    
