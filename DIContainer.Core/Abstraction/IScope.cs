using System;

namespace DIContainer.Core.Abstraction;

public interface IScope
{
    public object Resolve(Type @interface);
}