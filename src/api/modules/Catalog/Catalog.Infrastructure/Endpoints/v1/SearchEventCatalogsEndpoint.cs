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
            .Produces<EventCatalogResponse[]>(StatusCodes.Status200OK)
            .WithName("GetEventCatalogs")
            .WithOpenApi(x =>
            {
                x.Summary = "Gets a list of EventCatalogs.";
                return x;
            });
    }
}
