namespace Octopus.Catalog.Presentation.Http.Products;

public record ProductItemResponse
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Code { get; init; }
    public string Sku { get; init; }
}