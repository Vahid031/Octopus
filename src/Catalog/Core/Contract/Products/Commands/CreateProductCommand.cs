using MediatR;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Contract.Products.Commands;

public record CreateProductCommand(string Name, string Code, string Sku) : IRequest<ProductId>;