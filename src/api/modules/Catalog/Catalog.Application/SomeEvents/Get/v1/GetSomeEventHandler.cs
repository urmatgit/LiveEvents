using Microsoft.Extensions.DependencyInjection;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Caching;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Get.v1;

public sealed class GetSomeEventHandler(
    [FromKeyedServices("catalog:someevents")] IReadRepository<SomeEvent> repository,
    ICacheService cache)
    : IRequestHandler<GetSomeEventRequest, SomeEventResponse>
{
    public async Task<SomeEventResponse> Handle(GetSomeEventRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"someevent:{request.Id}",
            async () =>
            {
                var someEventItem = await repository.GetByIdAsync(request.Id, cancellationToken);
                if (someEventItem == null) throw new SomeEventNotFoundException(request.Id);
                return new SomeEventResponse(
                    someEventItem.Id,
                    someEventItem.Name,
                    someEventItem.Description,
                    someEventItem.OrganizationId,
                    someEventItem.MinParticipant,
                    someEventItem.MaxParticipant,
                    someEventItem.Durations,
                    someEventItem.Price,
                    someEventItem.EventDate,
                    someEventItem.EventCatalogId,
                    someEventItem.EventCatalog != null ? someEventItem.EventCatalog.Name : string.Empty);
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
