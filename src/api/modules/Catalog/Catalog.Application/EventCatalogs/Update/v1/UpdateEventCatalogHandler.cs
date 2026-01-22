using FSH.Framework.Core.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Update.v1;

public sealed class UpdateEventCatalogHandler(
    ILogger<UpdateEventCatalogHandler> logger,
    [FromKeyedServices("catalog:eventcatalogs")] IRepository<EventCatalog> repository)
    : IRequestHandler<UpdateEventCatalogCommand, UpdateEventCatalogResponse>
{
    public async Task<UpdateEventCatalogResponse> Handle(UpdateEventCatalogCommand request, CancellationToken cancellationToken)
    {
        var eventCatalog = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = eventCatalog ?? throw new NotFoundException($"EventCatalog with id : {request.Id} not found.");
        var updatedEventCatalog = eventCatalog.Update(request.Name, request.Description);
        await repository.UpdateAsync(updatedEventCatalog, cancellationToken);
        logger.LogInformation("EventCatalog with id : {EventCatalogId} updated.", eventCatalog.Id);
        return new UpdateEventCatalogResponse(eventCatalog.Id);
    }
}
