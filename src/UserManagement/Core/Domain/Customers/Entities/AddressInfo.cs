using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Customers.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Customers.Entities;

public class AddressInfo : EntityBase<AddressId>
{
    #region Properties

    public string Address { get; private protected set; }
    public GeoLocation GeoLocation { get; private protected set; }
    public CityInfo City { get; private protected set; }
    public CountryInfo Country { get; private protected set; }
    public ProvinceInfo Province { get; private protected set; }
    public bool IsDefault { get; private protected set; }

    #endregion
}
