using Octopus.Catalog.Core.Domain.Products.Entities;
using Octopus.Catalog.Core.Domain.Products.ValueObjects;
using Octopus.Core.Domain.Services;

namespace Octopus.Catalog.Core.Domain.Products.Services;

public interface IProductRepository : IRepository<Product, ProductId>
{
}
