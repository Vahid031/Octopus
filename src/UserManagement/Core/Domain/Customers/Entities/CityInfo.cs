using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Customers.Entities;

public class CityInfo : EntityBase<CityId>
{
    #region Properties

    public string Name { get; private protected set; }

    #endregion
}
