using DIContainer.Core.Abstraction;
using DIContainer.Core.Extensions;
using DIContainer.Core.Implementation;
using DIContainer.Tests.Abstractions;
using DIContainer.Tests.Models;
using FluentAssertions;
using Xunit;

namespace DIContainer.Tests.ContainerTests;

public class AddTransientTests
{
    [Fact]
    public void AddTransient_TInterface_TImplementation_NotNull()
    {
        ContainerBuilder builder = new ContainerBuilder();
        
        builder
            .AddTransient<IPersonService, PersonService>()
            .AddTransient<IRandomGuidService, RandomGuidService>()
            .AddTransient<ICarService, CarService>()
            .Build()
            .CreateScope()
            .Resolve<ICarService>()
            .Should()
            .NotBeNull();
    }
    
    [Fact]
    public void AddTransient_TInterface_TImplementation_Should_Return_Two_Different_Instances()
    {
        // arrange
        ContainerBuilder builder = new ContainerBuilder();
        
        // act
        var actualContainer = builder
            .AddTransient<IRandomGuidService, RandomGuidService>()
            .AddTransient<IPersonService, PersonService>()
            .AddTransient<ICarService, CarService>()
            .Build();
        
        var firstExpectedInstance = actualContainer
            .CreateScope()
            .Resolve<ICarService>();
            
        var secondExpectedInstance = actualContainer
            .CreateScope()
            .Resolve<ICarService>();
            
        // assert
        secondExpectedInstance
            .Should()
            .NotBeSameAs(firstExpectedInstance);
    }
    
    // todo do we need this test?
    [Fact]
    public void AddTransient_TInterface_TImplementation_Should_Return_Different_Instances()
    {
        // act
        ContainerBuilder builder = new ContainerBuilder();
        var actualContainer = builder
            .AddTransient<IRandomGuidService, RandomGuidService>()
            .AddTransient<IPersonService, PersonService>()
            .AddTransient<ICarService, CarService>()
            .Build();
            
        actualContainer
            .CreateScope()
            .Resolve<ICarService>()
            .Should()
            .NotBeNull();
        
        var firstGuid = actualContainer
            .CreateScope()
            .Resolve<ICarService>()
            .RandomGuid;
        
        var secondGuid = actualContainer
            .CreateScope()
            .Resolve<ICarService>()
            .RandomGuid;
            
        // assert
        var equals = firstGuid.Equals(secondGuid);
        equals.Should().Be(false);
    }
}