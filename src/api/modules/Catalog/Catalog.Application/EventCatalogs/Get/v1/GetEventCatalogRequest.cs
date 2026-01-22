using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Get.v1;

public class GetEventCatalogRequest : IRequest<EventCatalogResponse>
{
    public Guid Id { get; set; }
    public GetEventCatalogRequest(Guid id) => Id = id;
}
