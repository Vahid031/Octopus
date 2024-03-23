using Octopus.Core.Domain.Services;
using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Customers.Entities;

namespace Octopus.UserManagement.Core.Domain.Customers.Services;

public interface ICustomerRepository : IRepository<Customer, CustomerId>
{
}
