using MediatR;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Contract.Products.Notifications;

public record ProductCreatedNotification : INotification
{
    public ProductId Id { get; init; }
}
