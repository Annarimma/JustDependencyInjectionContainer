using System;
using DIContainer.Core.Builders;
using DIContainer.Core.Extensions;
using DIContainer.Core.MetaInfo;
using DIContainer.Tests.ContainerBuilderTests.Base;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests;

[TestFixture]
public class AsFixture : ContainerBuilderTestBase
{
    [Test]
    public void Register_ShouldReturn_CorrectResult()
    {
        var builder = ReflectiveBuilder;
        var result = builder
            .Register<Abc>()
            .Build()
            .CreateScope()
            .Resolve<Abc>();

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Abc));
    }
    
    [Test]
    public void RegisterAs_Should_Return_CorrectResult()
    {
        var builder = ReflectiveBuilder;
        var scope = builder
            .Register<Abc>()
            .As<IA>()
            .Build()
            .CreateScope();
        
        var isRegistered1 = scope.IsRegistered(typeof(Abc));

        var result = scope
            .Resolve<IA>();

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Abc));
        isRegistered1.Should().BeFalse();
    }
    
    [Test]
    public void RegisterNullInstance_Should_ThrowException()
    {
        var builder = ReflectiveBuilder;
        var descriptor = (ServiceMetaInfo)null;
        
        Action act = () => builder.Register(descriptor);

        act
            .Should()
            .Throw<ArgumentNullException>()
            .Where(e => e.Message.Contains(nameof(descriptor)));
    }
    
    // todo Register Type As Unsupported Service should throw Exception
    // [Test]
    // public void RegisterTypeAsUnsupportedService()
    // {
    //     var builder = ReflectiveBuilder;
    //     
    //     Action act = () => builder
    //         .Register<object>()
    //         .As<string>();
    //     
    //     act
    //         .Should()
    //         .Throw<ArgumentException>();
    // }
}