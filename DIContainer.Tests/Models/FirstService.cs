using System;
using DIContainer.Tests.Models.Abstraction;

namespace DIContainer.Tests.Models
{
    public class FirstService : IFirstService
    {
        public Guid RandomGuid { get; set; } = Guid.NewGuid();

        public FirstService(IRandomGuidService randomGuidService)
        {
            RandomGuid = randomGuidService.RandomGuid;
        }
    }
}