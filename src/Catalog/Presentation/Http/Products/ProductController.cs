using Microsoft.AspNetCore.Mvc;
using Octopus.Presentation.Http.EnvelopModels;

namespace Octopus.Catalog.Presentation.Http.Products;

[ApiController]
[Route("api/catalog/products")]
public class ProductController : ControllerBase
{
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
}

public record ProductResponse
{
    public int MyProperty { get; set; }
}