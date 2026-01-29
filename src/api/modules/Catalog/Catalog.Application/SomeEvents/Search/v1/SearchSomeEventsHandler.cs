using FSH.Framework.Core.Identity.Users.Abstractions;
using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Application.SomeEvents.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Search.v1;

public sealed class SearchSomeEventsHandler(
    IUserService userService,
    
    [FromKeyedServices("catalog:someevents")] IReadRepository<SomeEvent> repository)
    : IRequestHandler<SearchSomeEventsCommand, PagedList<SomeEventResponse>>
{
    public async Task<PagedList<SomeEventResponse>> Handle(SearchSomeEventsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new SearchSomeEventsSpecs(request);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var users =await  userService.GetListByIdsAsync(items
                                                            .Select(u => u.OrganizationId)
                                                            .Distinct()
                                                            .ToList(), cancellationToken);
        var itemUsers = items.Join(users,
            e => e.OrganizationId,
            u => u.Id,
            (e, u) => e with { OrganizationName = $"{u.FirstName} {u.LastName}" })
            .ToList();
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<SomeEventResponse>(itemUsers, request.PageNumber, request.PageSize, totalCount);
    }
}
