using Octopus.Catalog.Core.Domain.Products.ValueObjects;
using Octopus.Core.Domain.Entities;

namespace Octopus.Catalog.Core.Domain.Products.Entities;

public class ProductImageInfo : EntityBase<ProductImageId>
{
    #region Properties

    public string Url { get; private protected set; }

    public int PriorityDisplay { get; private protected set; }

    public bool IsDefault { get; private protected set; }

    #endregion

}