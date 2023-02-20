using System;
using DIContainer.Tests.TestContext.Abstractions;

namespace DIContainer.Tests.TestContext.Models
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