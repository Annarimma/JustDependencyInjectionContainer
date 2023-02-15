using DIContainer.Core.Abstraction;
using DIContainer.Core.Extensions;
using DIContainer.Core.Implementation;
using DIContainer.Tests.Abstractions;
using DIContainer.Tests.Models;
using FluentAssertions;
using Xunit;

namespace DIContainer.Tests.ContainerTests;

public class AddSingletonTests : TestsFixture
{
    [Fact]
    public void AddSingleton_TInterface_TImplementation_NotNull()
    {
        builder
            .AddSingleton<IPersonService, PersonService>()
            .AddSingleton<IRandomGuidService, RandomGuidService>()
            .AddSingleton<ICarService, CarService>()
            .Build()
            .CreateScope()
            .Resolve<ICarService>()
            .Should()
            .NotBeNull();
    }
    
    // [Fact]
    public void AddSingleton_TInterface_TImplementation_Should_Return_Two_Same_Instances()
    {
        IContainerBuilder containerBuilder = new ContainerBuilder();
    
        var actualContainer = containerBuilder
            .AddSingleton<IRandomGuidService, RandomGuidService>()
            .AddSingleton<IPersonService, PersonService>()
            .AddSingleton<ICarService, CarService>()
            .Build();
        
        var firstExpectedInstance = actualContainer
            .CreateScope()
            .Resolve<ICarService>();
        
        var secondExpectedInstance = actualContainer
            .CreateScope()
            .Resolve<ICarService>();
            
        secondExpectedInstance
            .Should()
            .BeSameAs(firstExpectedInstance);
    }
}