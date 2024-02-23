using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Octopus.Catalog.Core.Contract.Products.Commands;
using Octopus.Catalog.Core.Contract.Products.Queries;
using Octopus.Presentation.Http;
using Octopus.Presentation.Http.EnvelopModels;

namespace Octopus.Catalog.Presentation.Http.Products;

[ApiController]
[Route("api/catalog/products")]
public class ProductController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [ProducesResponseType(typeof(SuccessEnvelop<PaginationViewModel<ProductItemResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status404NotFound)]
    [HttpGet("page/{page_number}")]
    public async Task<ActionResult<PaginationViewModel<ProductItemResponse>>> GetProductItemsByFilter(
        [FromRoute(Name = "page_number")] int? pageNumber,
        [FromQuery(Name = "page_size")] int? pageSize,
        [FromQuery(Name = "name")] string name,
        CancellationToken cancellationToken
    )
    {
        var query = new GetProductItemsByFilterQuery
        {
            PageNumber = pageNumber ?? 1,
            PageSize = pageSize ?? 10,
            Name = name
        };
        var products = await mediator.Send(query, cancellationToken);

        if (!products.HasItem)
            return NotFound();

        var result = mapper.Map<PaginationViewModel<ProductItemResponse>>(products);

        return result;
    }

    [ProducesResponseType(typeof(SuccessEnvelop<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status404NotFound)]
    [HttpPost("")]
    public async Task<ActionResult<string>> CreateProduct(CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(request.Name, request.Code, request.Sku);
        var productId = await mediator.Send(command, cancellationToken);

        return productId.ToString();
    }
}