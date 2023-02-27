using DIContainer.Core.Extensions;
using DIContainer.Tests.TestContext.Abstractions;
using DIContainer.Tests.TestContext.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerTests.Lambda;

public class ContainerBuilderLambdaFixture : LambdaTestBase
{
    [Test]
    public void Container_Should_GetInstance()
    {
        Builder
            .AddSingleton<IPersonService, PersonService>()
            .AddSingleton<IRandomGuidService, RandomGuidService>()
            .AddSingleton<ICarService, CarService>()
            .Build()
            .CreateScope()
            .Resolve<ICarService>()
            .Should()
            .BeOfType<CarService>();
    }
    
    interface IA { }
    interface IB { }
    interface IC { }
    
    class Abc : DisposeTracker, IA, IB, IC { }
    
    [Test]
    public void AddSimpleRegistration_ShouldBe_NotNull()
    {
        var instance = Builder
            .AddTransient<Abc, Abc>()
            .Build()
            .CreateScope()
            .Resolve<Abc>();

        instance.Should().NotBeNull();
        instance.Should().BeOfType<Abc>();
    }

    [Test]
    public void Interface_ShouldBe_Registered()
    {
        var scope = Builder
            .AddTransient<IA, Abc>()
            .Build()
            .CreateScope();
        var instance = scope
            .Resolve<IA>();
        instance.Should().NotBeNull();
        instance.Should().BeOfType<Abc>();
        bool isRegisteredInstance = scope.IsRegistered<Abc>();
        isRegisteredInstance.Should().Be(false);
    }
    
    [Test]
    public void ScopeIsDisposed_And_SingletonInstancesAreNot_Disposed()
    {
        var container = Builder
            .AddSingleton<Abc, Abc>()
            .Build();
        var scope = container.CreateScope();
        var instance1 = scope.Resolve(typeof(Abc));
        var instance2 = scope.Resolve(typeof(Abc));

        scope.Dispose();

        bool isDisposed1 = ((Abc)instance1).IsDisposed;
        bool isDisposed2 = ((Abc)instance2).IsDisposed;
        
        isDisposed1.Should().BeFalse();
        isDisposed2.Should().BeFalse();
    }

    [Test]
    public void ScopeIsDisposed_And_TransientInstancesAre_Disposed()
    {
        var container = Builder
            .AddTransient<Abc, Abc>()
            .Build();
        var scope = container.CreateScope();
        var instance1 = scope.Resolve(typeof(Abc));
        var instance2 = scope.Resolve(typeof(Abc));

        scope.Dispose();

        bool isDisposed1 = ((Abc)instance1).IsDisposed;
        bool isDisposed2 = ((Abc)instance2).IsDisposed;
        
        isDisposed1.Should().BeTrue();
        isDisposed2.Should().BeTrue();
    }
    
    // [Test]
    public void ScopeIsDisposed_And_ScopedInstancesAre_Disposed()
    {
        var container = Builder
            .AddScoped<Abc, Abc>()
            .Build();
        var scope = container.CreateScope();
        var instance1 = scope.Resolve(typeof(Abc));
        var instance2 = scope.Resolve(typeof(Abc));

        scope.Dispose();

        ((Abc)instance1).IsDisposed.Should().BeTrue();
        ((Abc)instance2).IsDisposed.Should().BeTrue();
    }

    [Test]
    public void ContainerIsDisposed_And_TransientInstancesAreNot_Disposed()
    {
        var container = Builder
            .AddTransient<Abc, Abc>()
            .Build();
        var scope = container.CreateScope();
        var instance1 = scope.Resolve(typeof(Abc));
        var instance2 = scope.Resolve(typeof(Abc));

        container.Dispose();

        bool isDisposed1 = ((Abc)instance1).IsDisposed;
        bool isDisposed2 = ((Abc)instance2).IsDisposed;
        
        isDisposed1.Should().BeFalse();
        isDisposed2.Should().BeFalse();
    }
}