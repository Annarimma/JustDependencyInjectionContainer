using DIContainer.Core.Extensions;
using DIContainer.Tests.ContainerBuilderTests.Base;
using DIContainer.Tests.TestContext.Abstractions;
using DIContainer.Tests.TestContext.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests;

[TestFixture]
public class AddTransientFixture : ContainerBuilderTestBase
{
    [Test]
    public void TransientInstance_ShouldBe_NotNull()
    {
        foreach (var builder in Builders)
        {
            var instance = builder
                .AddTransient<IB, Abc>()
                .Build()
                .CreateScope()
                .Resolve<IB>();

            instance.Should().NotBeNull();
        }
    }

    [Test]
    public void TransientInstance_ShouldBe_OfCorrectType()
    {
        foreach (var builder in Builders)
        {
            builder
                .AddTransient<IB>(typeof(Abc))
                .Build()
                .CreateScope()
                .Resolve<IB>()
                .Should()
                .BeOfType<Abc>();
        }
    }

    [Test]
    public void AddSimpleTransientRegistration_ShouldBe_NotNull()
    {
        foreach (var builder in Builders)
        {
            var instance = builder
                .AddTransient<Abc, Abc>()
                .Build()
                .CreateScope()
                .Resolve<Abc>();

            instance.Should().NotBeNull();
            instance.Should().BeOfType<Abc>();
        }
    }

    // todo do it more consistently with Abc, IA ...
    [Test]
    public void AddTransientInstances_NotNull()
    {
        foreach (var builder in Builders)
        {
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
    }

    [Test]
    public void TransientInstancesInOneScope_ShouldBe_NotSame()
    {
        foreach (var builder in Builders)
        {
            var actualContainer = builder
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
    }

    [Test]
    public void TransientInstancesInScopes_ShouldBe_NotSame()
    {
        foreach (var builder in Builders)
        {
            var actualContainer = builder
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
}