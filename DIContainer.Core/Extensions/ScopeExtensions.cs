using DIContainer.Core.Abstraction;

namespace DIContainer.Core.Extensions;

public static class ScopeExtensions
{
    public static T Resolve<T>(this IScope scope) where T : class
        => (T)scope.Resolve(typeof(T));
}