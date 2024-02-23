namespace Octopus.Catalog.Presentation.Http.Products;

public record CreateProductRequest
{
    public string Name { get; init; }
    public string Code { get; init; }
    public string Sku { get; init; }
}