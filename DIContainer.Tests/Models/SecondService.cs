using System;
using DIContainer.Tests.Models.Abstraction;

namespace DIContainer.Tests.Models
{
    public class SecondService : ISecondService
    {
        private readonly IFirstService _firstService;
        private readonly IRandomGuidService _randomGuidService;
        public Guid RandomGuid { get; set; } = Guid.NewGuid();

        public SecondService(IFirstService firstService, IRandomGuidService randomGuidService)
        {
            _firstService = firstService;
            _randomGuidService = randomGuidService;
            RandomGuid = _randomGuidService.RandomGuid;
        }
    }
}