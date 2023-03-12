using DIContainer.Core.Extensions;
using DIContainer.Tests.ContainerBuilderTests.Base;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests;

[TestFixture]
public class IsRegisteredFixture : ContainerBuilderTestBase
{
    [Test]
    public void Interface_ShouldBe_RegisteredInScope()
    {
        foreach (var builder in Builders)
        {
            var scope = builder
                .AddScoped<IA, Abc>()
                .Build()
                .CreateScope();

            var instance = scope
                .Resolve<IA>();

            var isRegisteredTypeIA = scope.IsRegistered<IA>();

            instance.Should().NotBeNull();
            instance.Should().BeOfType<Abc>();

            isRegisteredTypeIA.Should().Be(true);
        }
    }

    [Test]
    public void Instance_ShouldNotBe_RegisteredInScope()
    {
        foreach (var builder in Builders)
        {
            var scope = builder
                .AddScoped<IA, Abc>()
                .Build()
                .CreateScope();

            var instance = scope
                .Resolve<IA>();

            var isRegisteredTypeAbc = scope.IsRegistered<Abc>();

            instance.Should().NotBeNull();
            instance.Should().BeOfType<Abc>();

            isRegisteredTypeAbc.Should().Be(false);
        }
    }
}