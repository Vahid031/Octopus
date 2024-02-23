using MediatR;
using Octopus.Catalog.Core.Domain.Products.ValueObjects;

namespace Octopus.Catalog.Core.Contract.Products;

public record CrreateProductCommand(string Name, string Code, string Sku) : IRequest<ProductId>;