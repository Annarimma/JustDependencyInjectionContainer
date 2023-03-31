using System;
using DIContainer.Core.Builders;
using DIContainer.Core.ErrorHandler;
using DIContainer.Tests.ContainerBuilderTests.Base;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests;

[TestFixture]
internal class ContainerBuilderTests : ContainerBuilderTestBase
{
	[Test]
	public void ContainerBuilder_ShouldNot_RegisterNull()
	{
		var builder = new ContainerBuilder();
		var act = () => builder.Register(null);
        act
            .Should()
            .Throw<ArgumentNullException>();
    }

	[Test]
	public void ExposesImplementationType()
	{
		var builder = new ContainerBuilder()
			.Register<Abc>()
			.As(typeof(object));
		var scope = builder
			.Build()
			.CreateScope();
		var instance = scope.Resolve(typeof(object));
		instance
			.Should()
			.BeOfType(typeof(Abc));
	}

	[Test]
	public void Container_ShouldBeIsBuilded_OnlyOnce()
	{
		var builder = new ContainerBuilder();
		builder.Build();

		var act = () => builder.Build();

		act
			.Should()
			.Throw<InjectionException>()
			.WithMessage(InjectionException.BuildShouldBeCalledOnce);
	}
}