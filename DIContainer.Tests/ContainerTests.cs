using DIContainer.Core.Implementation;
using DIContainer.Tests.Models;
using DIContainer.Tests.Models.Abstraction;
using FluentAssertions;
using Xunit;

namespace DIContainer.Tests
{
    public class ContainerTests
    {
        [Fact]
        public void AddSingleton_TInterface_TImplementation_NotNull()
        {
            var container = new ContainerBuilder();
            container
                .AddSingleton<IFirstService, FirstService>()
                .AddSingleton<IRandomGuidService, RandomGuidService>()
                .AddSingleton<ISecondService, SecondService>()
                .Build()
                .GetInstance<ISecondService>()
                .Should()
                .NotBeNull();
        }
        
        [Fact]
        public void AddSingleton_TInterface_TImplementation_Should_Return_Two_Same_Instances()
        {
            var containerBuilder = new ContainerBuilder();

            var generatedContainer = containerBuilder
                .AddSingleton<IRandomGuidService, RandomGuidService>()
                .AddSingleton<IFirstService, FirstService>()
                .AddSingleton<ISecondService, SecondService>()
                .Build();
            
            var expectedEntity = generatedContainer.GetInstance<ISecondService>();
            
            generatedContainer
                .GetInstance<ISecondService>()
                .Should()
                .BeSameAs(expectedEntity);
        }
        
        [Fact]
        public void AddTransient_TInterface_TImplementation_NotNull()
        {
            var container = new ContainerBuilder();
            container
                .AddTransient<IFirstService, FirstService>()
                .AddTransient<IRandomGuidService, RandomGuidService>()
                .AddTransient<ISecondService, SecondService>()
                .Build()
                .GetInstance<ISecondService>()
                .Should()
                .NotBeNull();
        }
        
        [Fact]
        public void AddTransient_TInterface_TImplementation_Should_Return_Two_Different_Instances()
        {
            var containerBuilder = new ContainerBuilder();

            var generatedContainer = containerBuilder
                .AddTransient<IRandomGuidService, RandomGuidService>()
                .AddTransient<IFirstService, FirstService>()
                .AddTransient<ISecondService, SecondService>()
                .Build();
            var expectedEntity = generatedContainer.GetInstance<ISecondService>();
            
            generatedContainer
                .GetInstance<ISecondService>()
                .Should()
                .NotBeSameAs(expectedEntity);
        }
        
        [Fact]
        public void JustTest()
        {
            var containerBuilder = new ContainerBuilder();

            var generatedContainer = containerBuilder
                .AddTransient<IRandomGuidService, RandomGuidService>()
                .AddTransient<IFirstService, FirstService>()
                .AddTransient<ISecondService, SecondService>()
                .Build();
            
            generatedContainer
                .GetInstance<ISecondService>()
                .Should()
                .NotBeNull();

            var a = generatedContainer
                .GetInstance<ISecondService>();

            var b = generatedContainer
                .GetInstance<ISecondService>();
            
            Assert.NotEqual(a.RandomGuid, b.RandomGuid);
        }
    }
}