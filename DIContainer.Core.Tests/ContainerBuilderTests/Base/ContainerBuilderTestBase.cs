using System.Collections.Generic;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Builders;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests.Base;

public class ContainerBuilderTestBase
{
    protected List<IContainerBuilder> Builders;
    protected IContainerBuilder ReflectiveBuilder;
    protected IContainerBuilder LambdaBuilder;

    [SetUp]
    public void BeforeEach()
    {
        ReflectiveBuilder = new ContainerBuilder(new ReflectionActivationBuilder());
        LambdaBuilder = new ContainerBuilder(new LambdaActivationBuilder());
        
        Builders = new List<IContainerBuilder>()
        {
            ReflectiveBuilder,
            LambdaBuilder,
        };
    }

    [TearDown]
    public void AfterEach()
    {
        ReflectiveBuilder = null;
        LambdaBuilder = null;
        Builders = new List<IContainerBuilder>();
    }
    
    protected interface IA { }
    protected interface IB { }
    protected interface IC { }

    protected class Abc : DisposeTracker, IA, IB, IC {}
}