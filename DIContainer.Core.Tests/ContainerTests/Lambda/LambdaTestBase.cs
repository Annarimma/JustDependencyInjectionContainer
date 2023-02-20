using DIContainer.Core.Builders;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerTests.Lambda;

public class LambdaTestBase
{
    protected ContainerBuilder Builder;

    [SetUp]
    public void BeforeEach()
    {
        Builder = new ContainerBuilder(new LambdaActivationBuilder());
    }

    [TearDown]
    public void AfterEach()
    {
        Builder = null;
    }
}