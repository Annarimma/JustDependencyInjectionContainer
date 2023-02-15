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
        public void Container_Should_GetInstance()
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
                .BeOfType<CarService>();
        }
        
        //[Fact]
        public void Container_Should_GetInstance_WithParams()
        {
            
        }
        
        //[Fact]
        public void Container_Should_GetInstance_WithNoParams()
        {
            
        }
    }
}