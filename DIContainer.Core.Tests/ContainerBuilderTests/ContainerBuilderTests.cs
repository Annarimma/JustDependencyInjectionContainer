using System;
using DIContainer.Core.Builders;
using DIContainer.Core.MetaInfo;
using DIContainer.Tests.ContainerBuilderTests.Base;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests;

[TestFixture]
public class ContainerBuilderTests : ContainerBuilderTestBase
{
    [Test]
    public void RegisterNull()
    {
        var target = new ContainerBuilder();
        Assert.Throws<ArgumentNullException>(() => target.Register(null));
    }
    
    [Test]
    public void ExposesImplementationType()
    {
        var builder = new ContainerBuilder();
        var r = builder.Register<Abc>().As(typeof(object));
        var scope = r.Build().CreateScope();
        var instance = scope.Resolve(typeof(object));
        instance.Should().BeOfType(typeof(Abc));
    }
}