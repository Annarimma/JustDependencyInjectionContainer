using DIContainer.Core.Extensions;
using DIContainer.Tests.Abstractions;
using DIContainer.Tests.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerTests;

[TestFixture]
public class AddSingletonTests : TestBase
{
    [Test]
    public void AddSingleton_TInterface_TImplementation_NotNull()
    {
        Builder
            .AddSingleton<IPersonService, PersonService>()
            .AddSingleton<IRandomGuidService, RandomGuidService>()
            .AddSingleton<ICarService, CarService>()
            .Build()
            .CreateScope()
            .Resolve<ICarService>()
            .Should()
            .NotBeNull();
    }
    
    //[Test]
    public void AddSingleton_TInterface_TImplementation_Should_Return_Two_Same_Instances()
    {
        var actualContainer = Builder
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