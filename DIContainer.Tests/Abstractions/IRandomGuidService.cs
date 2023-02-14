using System;

namespace DIContainer.Tests.Abstractions
{
    public interface IRandomGuidService
    {
        Guid RandomGuid { get; }
    }
}