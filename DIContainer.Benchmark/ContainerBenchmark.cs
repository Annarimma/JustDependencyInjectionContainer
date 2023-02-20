using Autofac;
using BenchmarkDotNet.Attributes;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Builders;
using DIContainer.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DIContainer.Benchmark;

[MemoryDiagnoser]
public class ContainerBenchmark
{
    private readonly IScope _lambdaContainer, _reflectionContainer;
    private readonly ILifetimeScope _scope;
    private readonly IServiceScope _serviceScope;
    
    public ContainerBenchmark()
    {
        var lambdaBuilder = new Core.Builders.ContainerBuilder(new LambdaActivationBuilder());
        var reflectionBuilder = new Core.Builders.ContainerBuilder(new ReflectionActivationBuilder());
        InitContainer(lambdaBuilder);
        InitContainer(reflectionBuilder);
        _reflectionContainer = reflectionBuilder.Build().CreateScope();
        _lambdaContainer = lambdaBuilder.Build().CreateScope();
        _scope = InitAutofac();
        _serviceScope = InitMSDI();
    }

    private void InitContainer(Core.Builders.ContainerBuilder builder)
    {
        builder
            .AddTransient<IService, Service>()
            .AddTransient<Controller, Controller>();
    }

    private ILifetimeScope InitAutofac()
    {
        Autofac.ContainerBuilder containerBuilder = new Autofac.ContainerBuilder();
        containerBuilder.RegisterType<Service>().As<IService>();
        containerBuilder.RegisterType<Controller>().AsSelf();
        return containerBuilder.Build().BeginLifetimeScope();
    }

    private IServiceScope InitMSDI()
    {
        var collection = new ServiceCollection();
        collection.AddTransient<IService, Service>();
        collection.AddTransient<Controller, Controller>();
        return collection.BuildServiceProvider().CreateScope();
    }

    [Benchmark(Baseline = true)]
    public Controller Create() => new Controller(new Service());
    
    [Benchmark]
    public Controller Reflection() => (Controller)_reflectionContainer.Resolve(typeof(Controller));

    [Benchmark]
    public Controller Lambda() => (Controller)_lambdaContainer.Resolve(typeof(Controller));

    [Benchmark]
    public Controller Autofac() => _scope.Resolve<Controller>();
    
    [Benchmark]
    public Controller MSDI() => _serviceScope.ServiceProvider.GetRequiredService<Controller>();
}