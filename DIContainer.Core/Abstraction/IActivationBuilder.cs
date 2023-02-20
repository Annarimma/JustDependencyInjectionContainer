using System;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Abstraction;

public interface IActivationBuilder
{
    Func<IScope, object> BuildActivation(ServiceMetaInfo descriptor);
}