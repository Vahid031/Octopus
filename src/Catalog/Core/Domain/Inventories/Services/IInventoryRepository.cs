using Octopus.Catalog.Core.Domain.Inventories.Entities;
using Octopus.Core.Domain.Services;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Inventories.Services;

public interface IInventoryRepository : IRepository<Inventory, InventoryId>
{
    Task<Inventory> GetByDistributionCenterId(DistributionCenterId id);
}
