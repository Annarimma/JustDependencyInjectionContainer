using System;
using DIContainer.Tests.Abstractions;

namespace DIContainer.Tests.Models
{
    public class PersonService : IPersonService
    {
        public Guid RandomGuid { get; set; } = Guid.NewGuid();

        public PersonService(IRandomGuidService randomGuidService)
        {
            RandomGuid = randomGuidService.RandomGuid;
        }
    }
}