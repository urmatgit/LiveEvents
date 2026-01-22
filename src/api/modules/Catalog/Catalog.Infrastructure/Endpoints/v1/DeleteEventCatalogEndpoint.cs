using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Delete.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1;

public static class DeleteEventCatalogEndpoint
{
    internal static RouteHandlerBuilder MapEventCatalogDeleteEndpoint(this IEndpointRouteBuilder app)
    {
        
        
        return app.MapDelete("/{id:guid}", async (
                [AsParameters] DeleteEventCatalogCommand command,
                ISender sender) =>
            {
                await sender.Send(command);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("DeleteEventCatalog")
            .WithOpenApi(x =>
            {
                x.Summary = "Deletes an EventCatalog.";
                return x;
            });
    }
}
