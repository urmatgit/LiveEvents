using Ardalis.Specification;
using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Specifications;
using FSH.Starter.WebApi.Catalog.Application.SomeEvents.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Search.v1;

public class SearchSomeEventsSpecs : EntitiesByPaginationFilterSpec<SomeEvent, SomeEventResponse>
{
    public SearchSomeEventsSpecs(SearchSomeEventsCommand command)
        : base(command) =>
        Query
            .Include(x => x.EventCatalog)
            .OrderBy(c => c.Name, !command.HasOrderBy())
            .Where(b => b.Name.Contains(command.Keyword), !string.IsNullOrEmpty(command.Keyword))
            .Where(c => c.EventCatalogId == command.EventCatalogId, command.EventCatalogId.HasValue);
            
}
