using Octopus.Basket.Core.Domain.ShoppingCarts.ValueObjects;
using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Basket.Core.Domain.ShoppingCarts.Entities;

public class CartItem : EntityBase<CartItemId>
{
    #region Properties

    public ProductInfo Product { get; private protected set; }

    public ProductCombinationId CombinationId { get; private protected set; }

    public LotId LotId { get; private protected set; }

    public int Quantity { get; private protected set; }

    public decimal SalesPrice { get; private protected set; }

    public decimal ConsumerPrice { get; private protected set; }

    public decimal BasePrice { get; private protected set; }


    #endregion
}
