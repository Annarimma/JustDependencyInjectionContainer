using DIContainer.Core.Extensions;
using DIContainer.Tests.ContainerBuilderTests.Base;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerBuilderTests;

[TestFixture]
public class DisposeFixture : ContainerBuilderTestBase
{
    [Test]
    public void ScopeIsDisposed_And_SingletonInstancesAreNot_Disposed()
    {
        foreach (var builder in Builders)
        {
            var container = builder
                .AddSingleton<IB, Abc>()
                .Build();
            var scope = container.CreateScope();
            var instance1 = scope.Resolve<IB>();
            var instance2 = scope.Resolve<IB>();
        
            scope.Dispose();
        
            var isDisposed1 = ((Abc)instance1).IsDisposed;
            var isDisposed2 = ((Abc)instance2).IsDisposed;
                
            isDisposed1.Should().BeFalse();
            isDisposed2.Should().BeFalse();
        }
    }
    
    // todo is realy true?
    // [Test]
    // public void ScopeIsDisposed_And_TransientInstancesAre_Disposed()
    // {
    //     foreach (var builder in Builders)
    //     {
    //         var container = builder
    //             .AddTransient<IA, Abc>()
    //             .Build();
    //         var scope = container.CreateScope();
    //         var instance1 = scope.Resolve<IA>();
    //         var instance2 = scope.Resolve<IA>();
    //
    //         scope.Dispose();
    //
    //         var isDisposed1 = ((Abc)instance1).IsDisposed;
    //         var isDisposed2 = ((Abc)instance2).IsDisposed;
    //         
    //         isDisposed1.Should().BeTrue();
    //         isDisposed2.Should().BeTrue();
    //     }
    // }
    
    // todo fix it
    // [Test]
    // public void ScopeIsDisposed_And_ScopedInstancesAre_Disposed()
    // {
    //     foreach (var builder in Builders)
    //     {
    //         var container = builder
    //             .AddScoped<IC, Abc>()
    //             .Build();
    //         var scope = container.CreateScope();
    //         
    //         var instance1 = scope.Resolve<IC>();
    //         var instance2 = scope.Resolve<IC>();
    //
    //         scope.Dispose();
    //         
    //         var isDisposed1 = ((Abc)instance1).IsDisposed;
    //         var isDisposed2 = ((Abc)instance2).IsDisposed;
    //         
    //         isDisposed1.Should().BeTrue();
    //         isDisposed2.Should().BeTrue();
    //     }
    // }
    
    [Test]
    public void ContainerIsDisposed_And_TransientInstancesAreNot_Disposed()
    {
        foreach (var builder in Builders)
        {
            var container = builder
                .AddTransient<IB, Abc>()
                .Build();
            var scope = container.CreateScope();
            var instance1 = scope.Resolve<IB>();
            var instance2 = scope.Resolve<IB>();
        
            container.Dispose();
        
            var isDisposed1 = ((Abc)instance1).IsDisposed;
            var isDisposed2 = ((Abc)instance2).IsDisposed;
            
            isDisposed1.Should().BeFalse();
            isDisposed2.Should().BeFalse();
        }
    }
}