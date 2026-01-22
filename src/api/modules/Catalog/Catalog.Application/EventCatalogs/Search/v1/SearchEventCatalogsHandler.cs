using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Search.v1;

public sealed class SearchEventCatalogsHandler(
    [FromKeyedServices("catalog:eventcatalogs")] IReadRepository<EventCatalog> repository)
    : IRequestHandler<SearchEventCatalogsCommand, PagedList<EventCatalogResponse>>
{
    public async Task<PagedList<EventCatalogResponse>> Handle(SearchEventCatalogsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new SearchEventCatalogSpecs(request);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<EventCatalogResponse>(items, request!.PageNumber, request!.PageSize, totalCount);
    }
}
