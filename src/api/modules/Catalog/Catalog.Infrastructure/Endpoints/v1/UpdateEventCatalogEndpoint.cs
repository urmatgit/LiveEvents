using FSH.Framework.Infrastructure.Auth.Policy;
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
                Guid id,
                 UpdateEventCatalogCommand command,
                ISender sender) =>
            {
                if (id != command.Id) return Results.BadRequest();
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .Produces<UpdateEventCatalogResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("update a EventCatalog")
            .WithDescription("update a EventCatalog")
            .WithName(nameof(UpdateEventCatalogEndpoint))
            .RequirePermission("Permissions.EventCatalogs.Update")
            .MapToApiVersion(1);
    }
}
