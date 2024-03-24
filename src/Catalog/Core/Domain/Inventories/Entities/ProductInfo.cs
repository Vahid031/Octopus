using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Inventories.Entities;

public class ProductInfo : EntityBase<ProductId>
{
    #region Properties

    public ProductCombinationId CombinationId { get; private protected set; }

    private List<LotInfo> _lots;
    public IReadOnlyCollection<LotInfo> Lots
    {
        get { return _lots.AsReadOnly(); }
        private protected set { _lots = value.ToList(); }
    }

    #endregion

}
