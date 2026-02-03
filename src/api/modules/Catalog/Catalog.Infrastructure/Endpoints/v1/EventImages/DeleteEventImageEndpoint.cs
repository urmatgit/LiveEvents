using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Delete.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.EventImages;

public static class DeleteEventImageEndpoint
{
    internal static RouteHandlerBuilder MapDeleteEventImageEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapDelete("/{id:guid}", async (
                [AsParameters] DeleteEventImageCommand command,
                ISender sender) =>
            {
                await sender.Send(command);
                return Results.NoContent();
            })
            .WithName(nameof(DeleteEventImageEndpoint))
            .WithSummary("deletes a event image by id")
            .WithDescription("deletes a event image by id")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .RequirePermission("Permissions.EventImages.Delete")
            .MapToApiVersion(1);
    }
}
