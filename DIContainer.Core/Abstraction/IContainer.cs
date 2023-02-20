using System;

namespace DIContainer.Core.Abstraction
{
    public interface IContainer: IDisposable, IAsyncDisposable
    {
        IScope CreateScope();
    }
}