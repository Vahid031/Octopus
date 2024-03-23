using Octopus.Catalog.Core.Domain.Products.Entities;
using Octopus.Core.Domain.Services;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Products.Services;

public interface IProductRepository : IRepository<Product, ProductId>
{
}
