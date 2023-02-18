using DIContainer.Core.Extensions;
using DIContainer.Tests.Abstractions;
using DIContainer.Tests.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerTests;

[TestFixture]
public class AddTransientTests : TestBase
{
    [Test]
    public void AddTransient_TInterface_TImplementation_NotNull()
    {
        Builder
            .AddTransient<IPersonService, PersonService>()
            .AddTransient<IRandomGuidService, RandomGuidService>()
            .AddTransient<ICarService, CarService>()
            .Build()
            .CreateScope()
            .Resolve<ICarService>()
            .Should()
            .NotBeNull();
    }
    
    [Test]
    public void AddTransient_TInterface_TImplementation_Should_Return_Two_Different_Instances()
    {
        // act
        var actualContainer = Builder
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
    [Test]
    public void AddTransient_TInterface_TImplementation_Should_Return_Different_Instances()
    {
        // act
        var actualContainer = Builder
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