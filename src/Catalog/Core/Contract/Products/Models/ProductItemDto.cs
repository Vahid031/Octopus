using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Contract.Products.Models;

public record ProductItemDto(ProductId Id, string Name, string Code, string Sku);