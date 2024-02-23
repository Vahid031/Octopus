using MediatR;
using Octopus.Catalog.Core.Domain.Products.ValueObjects;

namespace Octopus.Catalog.Core.Contract.Products.Commands;

public record CreateProductCommand(string Name, string Code, string Sku) : IRequest<ProductId>;