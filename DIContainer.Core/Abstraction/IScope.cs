using System;

namespace DIContainer.Core.Abstraction;

public interface IScope: IDisposable, IAsyncDisposable
{
    public object Resolve(Type @interface);
}