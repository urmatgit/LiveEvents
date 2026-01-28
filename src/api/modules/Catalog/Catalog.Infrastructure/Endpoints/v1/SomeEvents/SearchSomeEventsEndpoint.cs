using FSH.Framework.Core.Paging;
using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.SomeEvents.Get.v1;
using FSH.Starter.WebApi.Catalog.Application.SomeEvents.Search.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.SomeEvents;

public static class SearchSomeEventsEndpoint
{
    internal static RouteHandlerBuilder MapGetSomeEventListEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/search", async (
                [FromBody] SearchSomeEventsCommand command,
                ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithName(nameof(SearchSomeEventsEndpoint))
            .WithSummary("Gets a list of SomeEvents.")
            .WithDescription("Gets a list of SomeEvents.")
            .Produces<PagedList<SomeEventResponse>>()
            .RequirePermission("Permissions.SomeEvents.Search")
            .MapToApiVersion(1);
    }
}
