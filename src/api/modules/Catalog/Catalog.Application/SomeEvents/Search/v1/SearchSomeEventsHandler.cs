using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Application.SomeEvents.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Search.v1;

public sealed class SearchSomeEventsHandler(
    [FromKeyedServices("catalog:someevents")] IReadRepository<SomeEvent> repository)
    : IRequestHandler<SearchSomeEventsCommand, PagedList<SomeEventResponse>>
{
    public async Task<PagedList<SomeEventResponse>> Handle(SearchSomeEventsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new SearchSomeEventsSpecs(request);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<SomeEventResponse>(items, request.PageNumber, request.PageSize, totalCount);
    }
}
