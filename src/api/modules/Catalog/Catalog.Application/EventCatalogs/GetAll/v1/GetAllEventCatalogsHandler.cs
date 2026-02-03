using FSH.Framework.Core.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.GetAll.v1;

public class GetAllEventCatalogsHandler(
    [FromKeyedServices("catalog:eventcatalogs")] IReadRepository<EventCatalog> repository
    
    ) 
    : IRequestHandler<GetAllEventCatalogsRequest, List<EventCatalogResponse>>
{
    public async Task<List<EventCatalogResponse>> Handle(GetAllEventCatalogsRequest request, CancellationToken cancellationToken)
    {
        var eventCatalogs = await repository.ListAsync(cancellationToken);
        
        if (eventCatalogs == null)
        {
            throw new NotFoundException("EventCatalogs list not found.");
        }

        return eventCatalogs.Adapt<List<EventCatalogResponse>>();
    }
}
