using System;
using DIContainer.Tests.TestContext.Abstractions;

namespace DIContainer.Tests.TestContext.Models
{
    public class CarService : ICarService
    {
        private readonly IPersonService _personService;
        private readonly IRandomGuidService _randomGuidService;
        public Guid RandomGuid { get; set; } = Guid.NewGuid();

        public CarService(IPersonService personService, IRandomGuidService randomGuidService)
        {
            _personService = personService;
            _randomGuidService = randomGuidService;
            RandomGuid = _randomGuidService.RandomGuid;
        }
    }
}