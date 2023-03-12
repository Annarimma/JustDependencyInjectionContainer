using DIContainer.Core.Extensions;
using DIContainer.Tests.ContainerBuilderTests.Base;
using DIContainer.Tests.TestContext.Abstractions;
using DIContainer.Tests.TestContext.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests;

[TestFixture]
public class AddScopedFixture : ContainerBuilderTestBase
{
    [Test]
    public void Container_Should_GetScopedInstance() 
    {
        foreach (var builder in Builders)
        {
            builder
                .AddScoped<IPersonService, PersonService>()
                .AddScoped<IRandomGuidService, RandomGuidService>()
                .AddScoped<ICarService, CarService>()
                .Build()
                .CreateScope()
                .Resolve<ICarService>()
                .Should()
                .BeOfType<CarService>();
        }
    }
    
    [Test]
    public void TwoInstancesFromOneScope_ShouldBe_Same()
    {
        foreach (var builder in Builders)
        {
            var container = builder
                .AddScoped<IPersonService, PersonService>()
                .AddScoped<IRandomGuidService, RandomGuidService>()
                .AddScoped<ICarService, CarService>()
                .Build();
    
            var scope = container.CreateScope();
            var instance1 = scope.Resolve<ICarService>();
            var instance2 = scope.Resolve<ICarService>();
    
            instance1.Should().BeSameAs(instance2);
        }
    }
    
    [Test]
    public void TwoInstancesFromDifferentScope_ShouldNotBe_Same()
    {
        foreach (var builder in Builders)
        {
            var container = builder
                .AddScoped<IPersonService, PersonService>()
                .AddScoped<IRandomGuidService, RandomGuidService>()
                .AddScoped<ICarService, CarService>()
                .Build();
    
            var scope1 = container.CreateScope();
            var scope2 = container.CreateScope();
            var instance1 = scope1.Resolve<ICarService>();
            var instance2 = scope2.Resolve<ICarService>();
    
            instance1.Should().NotBeSameAs(instance2);
        }
    }
}