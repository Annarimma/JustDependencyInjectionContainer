using System;
using DIContainer.Tests.TestContext.Abstractions;

namespace DIContainer.Tests.TestContext.Models
{
    public class CarService : ICarService
    {
        private readonly IRandomGuidService _randomGuidService;
        public Guid RandomGuid { get; set; } = Guid.NewGuid();

        public CarService(IPersonService personService, IRandomGuidService randomGuidService)
        {
            _randomGuidService = randomGuidService;
            RandomGuid = _randomGuidService.RandomGuid;
        }
    }
}