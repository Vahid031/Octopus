using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Products.Entities;

public class ProductCombination : EntityBase<ProductCombinationId>
{
    #region Properties

    public string Sku { get; private protected set; }

    public string Gtin { get; private protected set; }

    public int SalesRatio { get; private protected set; }

    public decimal BasePrice { get; private protected set; }

    public decimal? ConsumerPrice { get; private protected set; }

    #endregion
}