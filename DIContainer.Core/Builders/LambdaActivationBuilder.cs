using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DIContainer.Core.Abstraction;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders;

/// <summary>
/// Lambda Activation.
/// </summary>
public class LambdaActivationBuilder : BaseActivationBuilder
{
    private static readonly MethodInfo ResolveMethod = typeof(IScope).GetMethod("Resolve");

    /// <summary>
    /// Lambda activation.
    /// </summary>
    /// <param name="typeDescriptor">Service Information.</param>
    /// <param name="ctor">Constructor Info.</param>
    /// <param name="args">Constructor parameters.</param>
    /// <returns>Delegate.</returns>
    protected override Func<IScope, object> BuildActivationInternal(
        TypeBasedServiceDescriptor typeDescriptor,
        ConstructorInfo ctor,
        ParameterInfo[] args)
    {
        if (typeDescriptor == null)
            throw new ArgumentNullException(nameof(typeDescriptor));

        if (ctor == null)
            throw new ArgumentNullException(nameof(ctor));

        if (args == null)
            throw new ArgumentNullException(nameof(args));

        var scopedParameter = Expression.Parameter(typeof(IScope), "scope");
        var ctorArgs = args
            .Select(x =>
                Expression.Convert(
                    Expression.Call(
                        scopedParameter,
                        ResolveMethod,
                        Expression.Constant(x.ParameterType)),
                    x.ParameterType));

        var @new = Expression.New(ctor, ctorArgs);

        var lambda = Expression.Lambda<Func<IScope, object>>(@new, scopedParameter);
        return lambda.Compile();
    }
}