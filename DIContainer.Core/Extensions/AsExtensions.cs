using DIContainer.Core.Abstraction;

namespace DIContainer.Core.Extensions;

/// <summary>
/// As Method Extensions.
/// </summary>
public static class AsExtensions
{
    public static IContainerBuilder As<TService>(this IContainerBuilder builder)
        where TService : notnull
    {
        if (builder == null)
            throw new System.ArgumentNullException(nameof(builder));

        return builder.As(typeof(TService));
    }
}