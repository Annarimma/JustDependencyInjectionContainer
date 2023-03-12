using System.Collections.Generic;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Builders;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests.Base;

public class ContainerBuilderTestBase
{
    protected List<IContainerBuilder> Builders;
    protected IContainerBuilder ReflectiveBuilder;

    [SetUp]
    public void BeforeEach()
    {
        ReflectiveBuilder = new ContainerBuilder(new ReflectionActivationBuilder());
        Builders = new List<IContainerBuilder>()
        {
            ReflectiveBuilder,
            new ContainerBuilder(new LambdaActivationBuilder()),
        };
    }

    [TearDown]
    public void AfterEach()
    {
        ReflectiveBuilder = null;
        Builders = new List<IContainerBuilder>();
    }
    
    protected interface IA { }
    protected interface IB { }
    protected interface IC { }

    protected class Abc : DisposeTracker, IA, IB, IC {}
}