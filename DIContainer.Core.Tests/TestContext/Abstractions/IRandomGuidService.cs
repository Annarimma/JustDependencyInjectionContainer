using System;

namespace DIContainer.Tests.Context.Abstractions
{
    public interface IRandomGuidService
    {
        Guid RandomGuid { get; }
    }
}