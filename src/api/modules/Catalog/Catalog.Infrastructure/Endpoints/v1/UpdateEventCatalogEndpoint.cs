using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Update.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1;

public static class UpdateEventCatalogEndpoint
{
    internal static RouteHandlerBuilder MapEventCatalogUpdateEndpoint(this IEndpointRouteBuilder app)
    {
        
        
        return app.MapPut("/{id:guid}", async (
                [AsParameters] UpdateEventCatalogCommand command,
                ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .Produces<UpdateEventCatalogResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("UpdateEventCatalog")
            .WithOpenApi(x =>
            {
                x.Summary = "Updates an existing EventCatalog.";
                return x;
            });
    }
}
