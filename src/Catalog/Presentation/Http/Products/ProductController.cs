using MediatR;
using Microsoft.AspNetCore.Mvc;
using Octopus.Catalog.Core.Contract.Products;
using Octopus.Presentation.Http.EnvelopModels;

namespace Octopus.Catalog.Presentation.Http.Products;

[ApiController]
[Route("api/catalog/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(SuccessEnvelop<List<ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status404NotFound)]
    [HttpGet("")]
    public async Task<ActionResult<List<ProductResponse>>> GetProducts()
    {

        var products = new List<ProductResponse>();

        products.Add(new ProductResponse { MyProperty = 12 });
        products.Add(new ProductResponse { MyProperty = 15 });

        return products;
    }

    [ProducesResponseType(typeof(SuccessEnvelop<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status404NotFound)]
    [HttpPost("")]
    public async Task<ActionResult<string>> CreateProduct(CreateProductRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new CrreateProductCommand(request.Name, request.Code, request.Sku);
        var productId = await _mediator.Send(command, cancellationToken);

        return productId.ToString();
    }
}

public record ProductResponse
{
    public int MyProperty { get; set; }
}

public record CreateProductRequest
{
    public string Name { get; init; }
    public string Code { get; init; }
    public string Sku { get; init; }
}