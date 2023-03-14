using System;

namespace DIContainer.Core.Abstraction;

/// <summary>
/// Dependencies managing container interface.
/// </summary>
public interface IContainer : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Creating instance scope.
    /// </summary>
    /// <returns><see cref="IScope"/> - Created scope.</returns>
    IScope CreateScope();
}