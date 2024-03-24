using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Inventories.Entities;

public class Inventory : AggregateRoot<InventoryId>
{
    #region Properties

    public DistributionCenterId DistributionCenterId { get; private protected set; }


    private List<ProductInfo> _products;
    public IReadOnlyCollection<ProductInfo> Products
    {
        get { return _products.AsReadOnly(); }
        private protected set { _products = value.ToList(); }
    }



    #endregion
}
