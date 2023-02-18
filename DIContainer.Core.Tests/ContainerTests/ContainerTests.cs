using DIContainer.Core.Extensions;
using DIContainer.Tests.Abstractions;
using DIContainer.Tests.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DIContainer.Tests.ContainerTests
{
    [TestFixture]
    public class ContainerTests : TestBase
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