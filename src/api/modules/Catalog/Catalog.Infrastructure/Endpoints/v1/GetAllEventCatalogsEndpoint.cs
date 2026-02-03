using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.GetAll.v1;
using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Get.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using FSH.Framework.Infrastructure.Auth.Policy;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1;

internal static class GetAllEventCatalogsEndpoint
{
    internal static RouteHandlerBuilder MapGetAllEventCatalogsEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapGet($"/all", 
                async (ISender sender) =>
            {
                var request = new GetAllEventCatalogsRequest();
                return Results.Ok(await sender.Send(request));
            })
            .WithName(nameof(GetAllEventCatalogsEndpoint))
            .WithDescription("Gets all EventCatalogs without pagination.")
            .Produces<List<EventCatalogResponse>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .RequirePermission("Permissions.EventCatalogs.View")
            .MapToApiVersion(1);

    }
}
