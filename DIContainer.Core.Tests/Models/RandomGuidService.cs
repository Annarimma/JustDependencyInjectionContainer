﻿using System;
using DIContainer.Tests.Abstractions;

namespace DIContainer.Tests.Models
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