using MediatR;
using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Get.v1;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.GetAll.v1;

public class GetAllEventCatalogsRequest : IRequest<List<EventCatalogResponse>>
{
}
