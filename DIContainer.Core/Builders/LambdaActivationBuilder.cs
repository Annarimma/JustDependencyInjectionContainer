using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DIContainer.Core.Abstraction;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders;

public class LambdaActivationBuilder : BaseActivationBuilder, IActivationBuilder
{
    private static readonly MethodInfo ResolveMethod = typeof(IScope).GetMethod("Resolve");

    protected override Func<IScope, object> BuildActivationInternal(TypeBasedServiceDescriptor typeDescriptor, ConstructorInfo ctor, ParameterInfo[] args)
    {
        var scopedParameter = Expression.Parameter(typeof(IScope), "scope");
        var ctorArgs = args
            .Select(x => 
                Expression.Convert(Expression.Call(scopedParameter,
                    ResolveMethod, 
                    Expression.Constant(x.ParameterType)), x.ParameterType));

        var @new = Expression.New(ctor, ctorArgs);
        
        var lambda = Expression.Lambda<Func<IScope, object>>(@new, scopedParameter);
        return lambda.Compile();
    }
}