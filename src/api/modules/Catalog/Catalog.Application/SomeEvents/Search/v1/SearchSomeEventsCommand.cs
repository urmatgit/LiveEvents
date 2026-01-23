using FSH.Framework.Core.Paging;
using FSH.Starter.WebApi.Catalog.Application.SomeEvents.Get.v1;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Search.v1;

public class SearchSomeEventsCommand : PaginationFilter, IRequest<PagedList<SomeEventResponse>>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid? EventCatalogId { get; set; }
}
