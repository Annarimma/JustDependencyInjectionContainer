using DIContainer.Core.Extensions;
using DIContainer.Tests.TestContext.Abstractions;
using DIContainer.Tests.TestContext.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerTests.Lambda;

public class AddTransientLambdaFixture : LambdaTestBase
{
    [Test]
    public void AddTransientInstances_NotNull()
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
    public void TransientInstancesInOneScope_ShouldBe_NotSame()
    {
        var actualContainer = Builder
            .AddTransient<IRandomGuidService, RandomGuidService>()
            .AddTransient<IPersonService, PersonService>()
            .AddTransient<ICarService, CarService>()
            .Build();

        var scope = actualContainer.CreateScope();
        
        var firstExpectedInstance = scope
            .Resolve<ICarService>();
            
        var secondExpectedInstance = scope
            .Resolve<ICarService>();
   
        firstExpectedInstance
            .Should()
            .NotBeSameAs(secondExpectedInstance);
    }
    
    [Test]
    public void TransientInstancesInScopes_ShouldBe_NotSame()
    {
        var actualContainer = Builder
            .AddTransient<IRandomGuidService, RandomGuidService>()
            .AddTransient<IPersonService, PersonService>()
            .AddTransient<ICarService, CarService>()
            .Build();

        var scope1 = actualContainer.CreateScope();
        var scope2 = actualContainer.CreateScope();
        
        var firstExpectedInstance = scope1
            .Resolve<ICarService>();
            
        var secondExpectedInstance = scope2
            .Resolve<ICarService>();
   
        firstExpectedInstance
            .Should()
            .NotBeSameAs(secondExpectedInstance);
    }
}