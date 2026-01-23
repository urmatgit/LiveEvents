using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Get.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1;

public static class GetEventCatalogEndpoint
{
    internal static RouteHandlerBuilder MapGetEventCatalogEndpoint(this IEndpointRouteBuilder app)
    {
       // var group = app.MapGroup("eventcatalogs").WithTags("eventcatalogs");
        
        return app.MapGet("/{id:guid}", async (
                [AsParameters] GetEventCatalogRequest request,
                ISender sender) =>
            {
                var result = await sender.Send(request);
                return Results.Ok(result);
            })
            .WithName(nameof(GetEventCatalogEndpoint))
            .WithSummary("Gets an EventCatalog by id.")
            .WithDescription("Gets an EventCatalog by id.")
            .Produces<EventCatalogResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .RequirePermission("Permissions.EventCatalogs.View")
            .MapToApiVersion(1);
    }
}
