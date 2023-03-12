using System;

namespace DIContainer.Tests;

public class DisposeTracker : IDisposable
{
    public bool IsDisposed;

    public void Dispose()
    {
        IsDisposed = true;
    }
}