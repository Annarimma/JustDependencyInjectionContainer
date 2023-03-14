using System;

namespace DIContainer.Tests.TestContext.Abstractions;

public interface ICarService
{
    public Guid RandomGuid { get; set; }
}