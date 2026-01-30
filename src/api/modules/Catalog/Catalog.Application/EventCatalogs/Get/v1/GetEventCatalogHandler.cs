using Microsoft.Extensions.DependencyInjection;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Caching;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Get.v1;

public sealed class GetEventCatalogHandler(
    [FromKeyedServices("catalog:eventcatalogs")] IReadRepository<EventCatalog> repository,
    ICacheService cache)
    : IRequestHandler<GetEventCatalogRequest, EventCatalogResponse>
{
    public async Task<EventCatalogResponse> Handle(GetEventCatalogRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"eventcatalog:{request.Id}",
            async () =>
            {
                var eventCatalogItem = await repository.GetByIdAsync(request.Id, cancellationToken);
                if (eventCatalogItem == null) throw new EventCatalogNotFoundException(request.Id);
                return new EventCatalogResponse(eventCatalogItem.Id, eventCatalogItem.Name, eventCatalogItem.Description, eventCatalogItem.ImageUrl);
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
