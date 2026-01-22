using FSH.Starter.WebApi.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.EventHandlers;

public class EventCatalogCreatedEventHandler(ILogger<EventCatalogCreatedEventHandler> logger) : INotificationHandler<EventCatalogCreated>
{
    public async Task Handle(EventCatalogCreated notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("handling eventcatalog created domain event..");
        await Task.FromResult(notification);
        logger.LogInformation("finished handling eventcatalog created domain event..");
    }
}
