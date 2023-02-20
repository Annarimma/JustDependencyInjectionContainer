using System;

namespace DIContainer.Tests.Context.Abstractions
{
    public interface IPersonService
    {
        public Guid RandomGuid { get; set; }
    }
}