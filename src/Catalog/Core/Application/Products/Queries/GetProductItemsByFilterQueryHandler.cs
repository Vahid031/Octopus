using MediatR;
using Octopus.Catalog.Core.Contract.Products.Models;
using Octopus.Catalog.Core.Contract.Products.Queries;
using Octopus.Catalog.Core.Contract.Products.Services;
using Octopus.Core.Contract.Queries;

namespace Octopus.Catalog.Core.Application.Products.Queries;

internal class GetProductItemsByFilterQueryHandler
    : IRequestHandler<GetProductItemsByFilterQuery, Pagination<ProductItemDto>>
{
    private readonly IProductDataService _productDataService;

    public GetProductItemsByFilterQueryHandler(IProductDataService productDataService)
    {
        _productDataService = productDataService;
    }

    public Task<Pagination<ProductItemDto>> Handle(GetProductItemsByFilterQuery request,
        CancellationToken cancellationToken)
    {
        return _productDataService.GetByFilter(request, cancellationToken);
    }
}