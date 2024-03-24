using Octopus.Basket.Core.Domain.ShoppingCarts.Enums;
using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Basket.Core.Domain.ShoppingCarts.Entities;

public class ShoppingCart : AggregateRoot<CartId>
{
    #region Properties

    public UserId UserId { get; private protected set; }

    public DistributionCenterId DistributionCenterId { get; private protected set; }

    public ShoppingCartStatusType Status { get; private protected set; }

    public int ToTalQuantity => _items.Sum(m => m.Quantity);
    public decimal ToTalPrice => _items.Sum(m => m.Quantity * m.SalesPrice);

    private List<CartItem> _items;
    public IReadOnlyCollection<CartItem> Items
    {
        get { return _items.AsReadOnly(); }
        private protected set { _items = value.ToList(); }
    }

    #endregion
}
