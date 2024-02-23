using Octopus.Catalog.Core.Contract.Products.Models;
using Octopus.Catalog.Core.Contract.Products.Queries;
using Octopus.Core.Contract.Queries;

namespace Octopus.Catalog.Core.Contract.Products.Services;

public interface IProductDataService
{
    Task<Pagination<ProductItemDto>> GetByFilter(GetProductItemsByFilterQuery query,
        CancellationToken cancellationToken);
}