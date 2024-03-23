using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Customers.Enums;

namespace Octopus.UserManagement.Core.Domain.Customers.Entities;

public class Customer : AggregateRoot<CustomerId>
{
    #region Properties

    public string FirstName { get; private protected set; }
    public string LastName { get; private protected set; }
    public string Code { get; private protected set; }
    public string PhoneNumber { get; private protected set; }
    public CustomerStatusType Status { get; private protected set; }
    public List<AddressInfo> Address { get; private protected set; }

    #endregion
}
