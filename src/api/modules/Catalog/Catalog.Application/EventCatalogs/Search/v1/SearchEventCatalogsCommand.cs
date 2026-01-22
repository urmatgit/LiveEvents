using FSH.Framework.Core.Paging;
using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Get.v1;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Search.v1;

public class SearchEventCatalogsCommand : PaginationFilter, IRequest<PagedList<EventCatalogResponse>>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
