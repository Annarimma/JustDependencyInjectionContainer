using DIContainer.Core.Abstraction;
using DIContainer.Core.Extensions;
using DIContainer.Core.Implementation;
using DIContainer.Tests.Abstractions;
using DIContainer.Tests.Models;
using FluentAssertions;
using Xunit;

namespace DIContainer.Tests.ContainerTests
{
    public class ContainerTests
    {
        [Fact]
        public void AddSingleton_TInterface_TImplementation_NotNull()
        {
            IContainerBuilder builder = new ContainerBuilder();
            builder
                .AddSingleton<IPersonService, PersonService>()
                .AddSingleton<IRandomGuidService, RandomGuidService>()
                .AddSingleton<ICarService, CarService>()
                .Build()
                .CreateScope()
                .Resolve<ICarService>()
                .Should()
                .NotBeNull();
        }
        
        // [Fact]
        // public void AddSingleton_TInterface_TImplementation_Should_Return_Two_Same_Instances()
        // {
        //     IContainerBuilder containerBuilder = new ContainerBuilder();
        //
        //     var generatedContainer = containerBuilder
        //         .AddSingleton<IRandomGuidService, RandomGuidService>()
        //         .AddSingleton<IPersonService, PersonService>()
        //         .AddSingleton<ICarService, CarService>()
        //         .Build();
        //     
        //     var expectedEntity = generatedContainer
        //         .CreateScope()
        //         .Resolve<ICarService>();
        //     
        //     generatedContainer
        //         .CreateScope()
        //         .Resolve<ICarService>()
        //         .Should()
        //         .BeSameAs(expectedEntity);
        // }
        
        [Fact]
        public void AddTransient_TInterface_TImplementation_NotNull()
        {
            var container = new ContainerBuilder();
            container
                .AddTransient<IPersonService, PersonService>()
                .AddTransient<IRandomGuidService, RandomGuidService>()
                .AddTransient<ICarService, CarService>()
                .Build()
                .CreateScope()
                .Resolve<ICarService>()
                .Should()
                .NotBeNull();
        }
        
        [Fact]
        public void AddTransient_TInterface_TImplementation_Should_Return_Two_Different_Instances()
        {
            var containerBuilder = new ContainerBuilder();
        
            var generatedContainer = containerBuilder
                .AddTransient<IRandomGuidService, RandomGuidService>()
                .AddTransient<IPersonService, PersonService>()
                .AddTransient<ICarService, CarService>()
                .Build();
            var expectedEntity = generatedContainer
                .CreateScope()
                .Resolve<ICarService>();
            
            generatedContainer
                .CreateScope()
                .Resolve<ICarService>()
                .Should()
                .NotBeSameAs(expectedEntity);
        }
        
        [Fact]
        public void JustTest()
        {
            var containerBuilder = new ContainerBuilder();
        
            var generatedContainer = containerBuilder
                .AddTransient<IRandomGuidService, RandomGuidService>()
                .AddTransient<IPersonService, PersonService>()
                .AddTransient<ICarService, CarService>()
                .Build();
            
            generatedContainer
                .CreateScope()
                .Resolve<ICarService>()
                .Should()
                .NotBeNull();
        
            var a = generatedContainer
                .CreateScope()
                .Resolve<ICarService>();
        
            var b = generatedContainer
                .CreateScope()
                .Resolve<ICarService>();
            
            Assert.NotEqual(a.RandomGuid, b.RandomGuid);
        }
    }
}