using FSH.Framework.Core.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Delete.v1;

public sealed class DeleteEventCatalogHandler(
    ILogger<DeleteEventCatalogHandler> logger,
    [FromKeyedServices("catalog:eventcatalogs")] IRepository<EventCatalog> repository)
    : IRequestHandler<DeleteEventCatalogCommand>
{
    public async Task Handle(DeleteEventCatalogCommand request, CancellationToken cancellationToken)
    {
        var eventCatalog = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = eventCatalog ?? throw new NotFoundException($"EventCatalog with id : {request.Id} not found.");
        await repository.DeleteAsync(eventCatalog, cancellationToken);
        logger.LogInformation("EventCatalog with id : {EventCatalogId} deleted", eventCatalog.Id);
    }
}
