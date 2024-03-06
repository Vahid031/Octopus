using MediatR;
using Octopus.Catalog.Core.Domain.Products.ValueObjects;

namespace Octopus.Catalog.Core.Contract.Products.Notifications;

public record ProductCreatedNotification : INotification
{
    public ProductId Id { get; init; }
}
