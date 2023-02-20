using System;
using DIContainer.Core.Abstraction;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders;

public class LambdaActivationBuilder : IActivationBuilder
{
    public Func<IScope, object> BuildActivation(ServiceMetaInfo descriptor)
    {
        throw new NotImplementedException();
    }
}