using System;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Implementation;

namespace DIContainer.Tests.ContainerTests;

public class TestsFixture : IDisposable
{
    protected ContainerBuilder builder;
    
    public TestsFixture ()
    {
        builder = new ContainerBuilder();
    }

    public void Dispose()
    {
        builder = null;
    }
}