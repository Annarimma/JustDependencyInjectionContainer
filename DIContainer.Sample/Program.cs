using DIContainer.Core.Abstraction;
using DIContainer.Core.Builders;
using DIContainer.Core.Extensions;
using DIContainer.Core.MetaInfo;
using DIContainer.Sample.Abstractions;
using DIContainer.Sample.Models;

#region Sample 1

var container = new ContainerBuilder(new ReflectionActivationBuilder())
    .AddSingleton<IRepository, Repository>()
    .AddScoped<IService, Service>()
    .Build();

var concreteInstance = container
    .CreateScope()
    .Resolve<IRepository>();
Console.WriteLine($"First sample result should return instance of Repository type: >>> {concreteInstance}");

#endregion



#region Sample 2

var ICInstance = new ContainerBuilder(new ReflectionActivationBuilder())
    .Register<CD>() // instance type
    .As<IC>() // register interface type to created instance type
    .Build()
    .CreateScope()
    .Resolve<IC>();
Console.WriteLine($"Second sample result should return instance of CD type: >>> {ICInstance}");

class CD : IC, ID {}
interface IC {}
interface ID {}

#endregion

    
