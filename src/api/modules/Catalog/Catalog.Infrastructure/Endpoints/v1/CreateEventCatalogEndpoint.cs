using FSH.Framework.Infrastructure.Auth.Policy;
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
                CreateEventCatalogCommand command,
                ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithName(nameof(CreateEventCatalogEndpoint))
            .WithSummary("creates a event catalog")
            .WithDescription("creates a event catalog")
            .Produces<CreateEventCatalogResponse>()
            //.ProducesProblem(StatusCodes.Status400BadRequest)
            .RequirePermission("Permissions.EventCatalogs.Create")
            .MapToApiVersion(1);
            //.WithOpenApi(x =>
            //{
            //    x.Summary = "Creates a new EventCatalog.";
            //    return x;
            //});
    }
}
