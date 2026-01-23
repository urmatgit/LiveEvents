using Ardalis.Specification;
using FSH.Framework.Core.Exceptions;
using FSH.Framework.Core.Persistence;

using FSH.Starter.WebApi.Catalog.Application.Specifications;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Create.v1;

public sealed class CreateEventCatalogHandler(
    ILogger<CreateEventCatalogHandler> logger,
    [FromKeyedServices("catalog:eventcatalogs")] IRepository<EventCatalog> repository, [FromKeyedServices("catalog:eventcatalogs")] IReadRepository<EventCatalog> readRepository)
    : IRequestHandler<CreateEventCatalogCommand, CreateEventCatalogResponse>
{
    public async Task<CreateEventCatalogResponse> Handle(CreateEventCatalogCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var getByNameSpec = new GetByNameSpecification<EventCatalog>(request.Name);
        var exitEventCatalog = await readRepository.ListAsync(getByNameSpec, cancellationToken);
        if (exitEventCatalog != null && exitEventCatalog.Count>0)
        {
            throw new CustomException($"{request.Name} already exists!");
        }
        var eventCatalog = EventCatalog.Create(request.Name!, request.Description);
        await repository.AddAsync(eventCatalog, cancellationToken);
        logger.LogInformation("eventcatalog created {EventCatalogId}", eventCatalog.Id);
        return new CreateEventCatalogResponse(eventCatalog.Id);
    }
     
    
}
