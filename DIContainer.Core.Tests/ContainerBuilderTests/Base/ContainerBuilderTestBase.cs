using System.Collections.Generic;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Builders;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests.Base;

public class ContainerBuilderTestBase
{
    protected List<IContainerBuilder> Builders;

    [SetUp]
    public void BeforeEach()
    {
        Builders = new List<IContainerBuilder>()
        {
            new ContainerBuilder(new ReflectionActivationBuilder()),
            new ContainerBuilder(new LambdaActivationBuilder()),
        };
    }

    [TearDown]
    public void AfterEach()
    {
        Builders = new List<IContainerBuilder>();
    }
    
    protected interface IA { }
    protected interface IB { }
    protected interface IC { }

    protected class Abc : DisposeTracker, IA, IB, IC {}
}