using MediatR;
using Octopus.Catalog.Core.Contract.Products.Models;
using Octopus.Core.Contract.Queries;

namespace Octopus.Catalog.Core.Contract.Products.Queries;

public record GetProductItemsByFilterQuery : PaginationFilterBase, IRequest<Pagination<ProductItemDto>>
{
    public string Name { get; init; }
}