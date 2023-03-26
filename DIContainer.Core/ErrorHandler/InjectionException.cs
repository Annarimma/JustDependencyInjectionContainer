using System;

namespace DIContainer.Core.ErrorHandler;

/// <summary>
/// Special injection exception.
/// </summary>
public class InjectionException : Exception
{
    public const string CanNotInstantiateInterface = "Interface or abstract class \"{0}\" cannot be instantiated";
    public const string MissingDependency = "\"{0}\" is missing";
    public const string DependencyAlreadyIsAdded = "\"{0}\" already is registered.";
    public const string RegistrationIsNotFound = "\"{0}\" Registration is not found.";
    public const string BuildShouldBeCalledOnce = "Build should be called once.";

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