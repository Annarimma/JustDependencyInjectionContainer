using System;
using System.Collections.Generic;
using System.Linq;
using DIContainer.Core.Abstraction;
using DIContainer.Core.Enums;
using DIContainer.Core.Extensions;
using DIContainer.Core.MetaInfo;

namespace DIContainer.Core.Builders;

/// <summary>
/// Registration Data about instance type and its interfaces type.
/// </summary>
public class RegistrationData
{
    private readonly ServiceMetaInfo _defaultService;
    private readonly ICollection<ServiceMetaInfo> _services = new HashSet<ServiceMetaInfo>();
    private bool _isDefaultServiceOverridden;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationData"/> class.
    /// </summary>
    /// <param name="defaultService">Default Interface type descriptor of instance type.</param>
    /// <exception cref="ArgumentNullException">If default Interface type is null.</exception>
    public RegistrationData(ServiceMetaInfo defaultService)
    {
        _defaultService = defaultService ?? throw new ArgumentNullException(nameof(defaultService));
    }

    /// <summary>
    /// Gets the interfaces explicitly assigned to the component.
    /// </summary>
    public IEnumerable<ServiceMetaInfo> Services
        => _isDefaultServiceOverridden ? _services : _services.DefaultIfEmpty(_defaultService);

    /// <summary>
    /// Overrides default interface type.
    /// </summary>
    /// <param name="interface">Interface type.</param>
    /// <returns>New default interface type of instance type.</returns>
    /// <exception cref="ArgumentNullException">If interface type is null.</exception>
    public ServiceMetaInfo RegisterAs(Type @interface)
    {
        if (@interface == null)
        {
            throw new ArgumentNullException(nameof(@interface));
        }

        _defaultService.InterfaceType = @interface;
        _isDefaultServiceOverridden = true;

        return _defaultService;
    }
}