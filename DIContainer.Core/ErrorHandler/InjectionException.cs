using System;

namespace DIContainer.Core.ErrorHandler
{
    /// <summary>
    /// Special injection exception.
    /// </summary>
    public class InjectionException : Exception
    {
        public const string CANNOT_INSTANTIATE_INTERFACE = "Interface or abstract class \"{0}\" cannot be instantiated";
        public const string MISSING_DEPENDENCY = "\"{0}\" is missing";
        public const string DEPENDENCY_ALREADY_IS_ADDED = "\"{0}\" already is registered.";
        public const string REGISTRATION_IS_NOT_FOUND = "\"{0}\" Registration is not found.";
        public const string BUILD_SHOULD_BE_CALLED_ONCE = "Build should be called once.";

        public InjectionException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }

        public InjectionException() : base()
        {
        }

        public InjectionException(string message) : base(message)
        {
        }
    }
}