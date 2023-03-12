using DIContainer.Core.Abstraction;
using DIContainer.Core.Builders;
using DIContainer.Core.Enums;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Extensions;

/// <summary>
/// As Method Extensions.
/// </summary>
public static class AsExtensions
{
    public static IContainerBuilder As<TService>(this IContainerBuilder builder)
        where TService : notnull
    {
        return builder.As(typeof(TService));
    }
}