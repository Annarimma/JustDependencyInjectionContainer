using DIContainer.Core.Implementation;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerTests;

public class TestBase
{
    protected ContainerBuilder Builder;

    [SetUp]
    public void BeforeEach()
    {
        Builder = new ContainerBuilder();
    }

    [TearDown]
    public void AfterEach()
    {
        Builder = null;
    }
}