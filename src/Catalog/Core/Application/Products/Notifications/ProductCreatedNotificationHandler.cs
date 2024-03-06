using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Catalog.Core.Contract.Products.Notifications;

namespace Octopus.Catalog.Core.Application.Products.Notifications;

internal class ProductCreatedNotificationHandler : INotificationHandler<ProductCreatedNotification>
{
    private readonly ILogger<ProductCreatedNotificationHandler> _logger;

    public ProductCreatedNotificationHandler(ILogger<ProductCreatedNotificationHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Product Created Succesfully, id: '{id}'", notification.Id);

        return Task.CompletedTask;
    }
}
