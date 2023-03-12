using System;
using DIContainer.Core.MetaInfo;
using DIContainer.Tests.ContainerBuilderTests.Base;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests;

[TestFixture]
public class ServiceDescriptorsFixture : ContainerBuilderTestBase
{
    [Test]
    public void InstanceBasedServiceDescriptor_ToString()
    {
        var instance = new InstanceBasedServiceDescriptor(typeof(IC), new Abc());
        var result = instance.ToString();
        result.Should().Be(typeof(IC).ToString());
    }

    [Test]
    public void TypeBasedServiceDescriptor_ToString()
    {
        var instance = new TypeBasedServiceDescriptor(typeof(IC));
        var result = instance.ToString();
        result.Should().Be(typeof(IC).ToString());
    }

    // todo this implementation
    // [Test]
    // public void FactoryBasedServiceDescriptor_ToString()
    // {
    //     var instance = new FactoryBasedServiceDescriptor();
    //     var result = instance.ToString();
    //     result.Should().Be(typeof(IC).ToString());
    // }
}