using System;
using DIContainer.Tests.TestContext.Abstractions;

namespace DIContainer.Tests.TestContext.Models
{
    public class RandomGuidService : IRandomGuidService
    {
        public Guid RandomGuid { get; } = Guid.NewGuid();

        // public RandomGuidService()
        // {
        //     
        // }
        //
        // public RandomGuidService(string guid)
        // {
        //     
        // }
        //
        // public RandomGuidService(string guidString, Guid guid)
        // {
        //     
        // }
    }
}