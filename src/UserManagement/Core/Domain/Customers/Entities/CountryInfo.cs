using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Customers.Entities;

public class CountryInfo : EntityBase<CountryId>
{
    #region Properties

    public string Name { get; private protected set; }

    #endregion
}
