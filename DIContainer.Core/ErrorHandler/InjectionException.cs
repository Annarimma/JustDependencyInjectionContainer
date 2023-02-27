﻿using System;

namespace DIContainer.Core.ErrorHandler
{
    /// <summary>
    /// Special injection exception.
    /// </summary>
    public class InjectionException : Exception
    {
        public const string CANNOT_INSTANTIATE_INTERFACE = "Interface or abstract class \"{0}\" cannot be instantiated";
        public const string MISSING_DEPENDENCY = "\"{0}\" is missing";

        public InjectionException(string message, Exception innerException = null)
            : base(message, innerException) { }
    }
}