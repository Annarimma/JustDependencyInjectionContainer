using DIContainer.Core.Extensions;
using DIContainer.Tests.TestContext.Abstractions;
using DIContainer.Tests.TestContext.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerTests;

[TestFixture]
public class AddSingletonTests : TestBase
{
    [Test]
    public void AddSingletonInstance_NotNull()
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
    
    [Test]
    public void SingletonInstancesFromOneScope_ShouldBe_Same()
    {
        var actualContainer = Builder
            .AddSingleton<IRandomGuidService, RandomGuidService>()
            .AddSingleton<IPersonService, PersonService>()
            .AddSingleton<ICarService, CarService>()
            .Build();

        var scope = actualContainer.CreateScope();
        
        var firstExpectedInstance = scope
            .Resolve<ICarService>();
        
        var secondExpectedInstance = scope
            .Resolve<ICarService>();
            
        firstExpectedInstance
            .Should()
            .BeSameAs(secondExpectedInstance);
    }
    
    [Test]
    public void SingletonInstancesFromScopes_ShouldBe_Same()
    {
        var actualContainer = Builder
            .AddSingleton<IRandomGuidService, RandomGuidService>()
            .AddSingleton<IPersonService, PersonService>()
            .AddSingleton<ICarService, CarService>()
            .Build();

        var scope1 = actualContainer.CreateScope();
        var scope2 = actualContainer.CreateScope();
        
        var firstExpectedInstance = scope1
            .Resolve<ICarService>();
        
        var secondExpectedInstance = scope2
            .Resolve<ICarService>();
            
        firstExpectedInstance
            .Should()
            .BeSameAs(secondExpectedInstance);
    }
}