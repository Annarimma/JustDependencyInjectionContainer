using DIContainer.Core.Builders;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerTests.Reflection;

public class ReflectionTestBase
{
    protected ContainerBuilder Builder;

    [SetUp]
    public void BeforeEach()
    {
        Builder = new ContainerBuilder(new ReflectionActivationBuilder());
    }

    [TearDown]
    public void AfterEach()
    {
        Builder = null;
    }
}