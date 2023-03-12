using DIContainer.Core.Extensions;
using DIContainer.Tests.ContainerBuilderTests.Base;
using DIContainer.Tests.TestContext.Abstractions;
using DIContainer.Tests.TestContext.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests;

[TestFixture]
public class AddSingletonFixture : ContainerBuilderTestBase
{
    [Test]
    public void Container_Should_GetSingletonInstance() 
    {
        foreach (var builder in Builders)
        {
            builder
                .AddSingleton<IPersonService, PersonService>()
                .AddSingleton<IRandomGuidService, RandomGuidService>()
                .AddSingleton<ICarService, CarService>()
                .Build()
                .CreateScope()
                .Resolve<ICarService>()
                .Should()
                .BeOfType<CarService>();
        }
    }
    
    [Test]
    public void AddSingletonInstance_NotNull()
    {
        foreach (var builder in Builders)
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
    }
    
    [Test]
    public void SingletonInstancesFromOneScope_ShouldBe_Same()
    {
        foreach (var builder in Builders)
        {
            var actualContainer = builder
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
    }
    
    [Test]
    public void SingletonInstancesFromScopes_ShouldBe_Same()
    {
        foreach (var builder in Builders)
        {
            var actualContainer = builder
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
}