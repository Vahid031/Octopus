using MediatR;
using Octopus.Catalog.Core.Domain.Products.ValueObjects;

namespace Octopus.Catalog.Core.Application.Products.Events;

public record ProductCreatedNotification : INotification
{
    public ProductId Id { get; init; }
}
