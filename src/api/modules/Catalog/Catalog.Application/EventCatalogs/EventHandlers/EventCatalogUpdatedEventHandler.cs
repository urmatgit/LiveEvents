using FSH.Starter.WebApi.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.EventHandlers;

public class EventCatalogUpdatedEventHandler(ILogger<EventCatalogUpdatedEventHandler> logger) : INotificationHandler<EventCatalogUpdated>
{
    public async Task Handle(EventCatalogUpdated notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("handling eventcatalog updated domain event..");
        await Task.FromResult(notification);
        logger.LogInformation("finished handling eventcatalog updated domain event..");
    }
}
