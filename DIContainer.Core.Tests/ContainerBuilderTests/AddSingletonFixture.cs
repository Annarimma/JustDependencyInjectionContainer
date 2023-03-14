using System;
using DIContainer.Core.ErrorHandler;
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
                .AddSingleton<IA, Abc>()
                .Build()
                .CreateScope()
                .Resolve<IA>()
                .Should()
                .BeOfType<Abc>();
        }
    }

    [Test]
    public void AddSingleton_TypeBasedRegistration_ShouldReturn_NotNull()
    {
        foreach (var builder in Builders)
        {
            builder
                .AddSingleton<IA, Abc>()
                .Build()
                .CreateScope()
                .Resolve<IA>()
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
                .AddSingleton<IA, Abc>()
                .Build();

            var scope = actualContainer.CreateScope();

            var firstExpectedInstance = scope
                .Resolve<IA>();

            var secondExpectedInstance = scope
                .Resolve<IA>();

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
                .AddSingleton<IC, Abc>()
                .Build();

            var scope1 = actualContainer.CreateScope();
            var scope2 = actualContainer.CreateScope();

            var firstExpectedInstance = scope1
                .Resolve<IC>();

            var secondExpectedInstance = scope2
                .Resolve<IC>();

            firstExpectedInstance
                .Should()
                .BeSameAs(secondExpectedInstance);
        }
    }

    [Test]
    public void DuplicationSingleton_TypeBasedRegistration_Should_ThrowException()
    {
        var builder = ReflectiveBuilder;

        var act = () =>
        {
            builder
                .AddSingleton(typeof(IA), typeof(Abc));

            builder
                .AddSingleton<IA>(typeof(IA))
                .Build();
        };

        act
            .Should()
            .Throw<InjectionException>()
            .Where(e
                => e.Message.Contains(InjectionException.DEPENDENCY_ALREADY_IS_ADDED));
    }

    [Test]
    public void DuplicationSingleton_InstanceRegistration_Should_ThrowException()
    {
        var builder = ReflectiveBuilder;

        var act = () =>
        {
            builder
                .AddSingleton(typeof(IA), new Abc());

            builder
                .AddSingleton<IA>(new Abc())
                .Build();
        };

        act
            .Should()
            .Throw<InjectionException>()
            .Where(e
                => e.Message.Contains(InjectionException.DEPENDENCY_ALREADY_IS_ADDED));
    }

    [Test]
    public void DuplicationSingleton_LambdaRegistration_Should_ThrowException()
    {
        var builder = ReflectiveBuilder;

        var act = () =>
        {
            builder
                .AddSingleton(typeof(IA), _ => new Abc());

            builder
                .AddSingleton<IA>(_ => new Abc())
                .Build();
        };

        act
            .Should()
            .Throw<InjectionException>()
            .Where(e
                => e.Message.Contains(InjectionException.DEPENDENCY_ALREADY_IS_ADDED));
    }
}