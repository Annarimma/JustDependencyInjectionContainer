using System;

namespace DIContainer.Tests.TestContext.Abstractions
{
    public interface IRandomGuidService
    {
        Guid RandomGuid { get; }
    }
}