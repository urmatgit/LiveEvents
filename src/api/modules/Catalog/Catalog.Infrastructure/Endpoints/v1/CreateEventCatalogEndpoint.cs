using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1;

public static class CreateEventCatalogEndpoint
{
    internal static RouteHandlerBuilder MapEventCatalogCreationEndpoint(this IEndpointRouteBuilder app)
    {
        
        
        return app.MapPost("/", async (
                [FromBody] CreateEventCatalogCommand command,
                ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .Produces<CreateEventCatalogResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("CreateEventCatalog")
            .WithOpenApi(x =>
            {
                x.Summary = "Creates a new EventCatalog.";
                return x;
            });
    }
}
