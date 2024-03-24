using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Basket.Core.Domain.ShoppingCarts.Entities;

public class ProductInfo : EntityBase<ProductId>
{
    #region Properties
    
    public string Name { get; private protected set; }

    #endregion
}