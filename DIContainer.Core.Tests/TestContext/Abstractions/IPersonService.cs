using System;

namespace DIContainer.Tests.TestContext.Abstractions;

public interface IPersonService
{
    public Guid RandomGuid { get; set; }
}