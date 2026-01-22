using FSH.Framework.Core.Paging;
using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Get.v1;
using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Search.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1;

public static class SearchEventCatalogsEndpoint
{
    internal static RouteHandlerBuilder MapGetEventCatalogListEndpoint(this IEndpointRouteBuilder app)
    {
        
        
        return app.MapGet("/search", async (
                [FromBody] SearchEventCatalogsCommand command,
                ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithName(nameof(SearchEventCatalogsEndpoint))
            .Produces<PagedList<EventCatalogResponse>>(StatusCodes.Status200OK)
            .RequirePermission("Permissions.EventCatalog.View")
            .WithOpenApi(x =>
            {
                x.Summary = "Gets a list of EventCatalogs.";
                x.Description= "Gets a list of EventCatalogs.";
                return x;
            })
            .MapToApiVersion(1);
    }
}
