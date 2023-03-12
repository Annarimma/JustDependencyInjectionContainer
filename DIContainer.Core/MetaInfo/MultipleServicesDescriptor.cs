using System;

namespace DIContainer.Core.MetaInfo;

public class MultipleServicesDescriptor : ServiceMetaInfo
{
    public ServiceMetaInfo[] Descriptors { get; init; }
    protected override string Description => throw new NotImplementedException();
}