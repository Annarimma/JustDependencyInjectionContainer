using System;

namespace DIContainer.Tests.Models.Abstraction
{
    public interface IRandomGuidService
    {
        Guid RandomGuid { get; }
    }
}