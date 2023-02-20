using DIContainer.Core.Extensions;
using DIContainer.Tests.TestContext.Abstractions;
using DIContainer.Tests.TestContext.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerTests.Reflection
{
    [TestFixture]
    public class ContainerReflectionTests : ReflectionTestBase
    {
        [Test]
        public void Container_Should_GetInstance()
        {
            Builder
                .AddSingleton<IPersonService, PersonService>()
                .AddSingleton<IRandomGuidService, RandomGuidService>()
                .AddSingleton<ICarService, CarService>()
                .Build()
                .CreateScope()
                .Resolve<ICarService>()
                .Should()
                .BeOfType<CarService>();
        }
    }
}