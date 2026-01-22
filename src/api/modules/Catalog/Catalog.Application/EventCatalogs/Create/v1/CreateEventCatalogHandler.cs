using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Exceptions;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Create.v1;

public sealed class CreateEventCatalogHandler(
    ILogger<CreateEventCatalogHandler> logger,
    [FromKeyedServices("catalog:eventcatalogs")] IRepository<EventCatalog> repository)
    : IRequestHandler<CreateEventCatalogCommand, CreateEventCatalogResponse>
{
    public async Task<CreateEventCatalogResponse> Handle(CreateEventCatalogCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var eventCatalog = EventCatalog.Create(request.Name!, request.Description);
        await repository.AddAsync(eventCatalog, cancellationToken);
        logger.LogInformation("eventcatalog created {EventCatalogId}", eventCatalog.Id);
        return new CreateEventCatalogResponse(eventCatalog.Id);
    }
}
