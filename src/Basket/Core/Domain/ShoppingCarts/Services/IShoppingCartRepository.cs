using Octopus.Basket.Core.Domain.ShoppingCarts.Entities;
using Octopus.Core.Domain.Services;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Basket.Core.Domain.ShoppingCarts.Services;

public interface IShoppingCartRepository : IRepository<ShoppingCart, CartId>
{
}
