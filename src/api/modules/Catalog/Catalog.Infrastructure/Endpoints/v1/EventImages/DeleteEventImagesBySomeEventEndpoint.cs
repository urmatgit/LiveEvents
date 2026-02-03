using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Delete.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.EventImages;

public static class DeleteEventImagesBySomeEventEndpoint
{
    internal static RouteHandlerBuilder MapDeleteEventImagesBySomeEventEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapDelete("/by-someevent/{someEventId:guid}", async (
                [AsParameters] DeleteEventImagesBySomeEventCommand command,
                ISender sender) =>
            {
                await sender.Send(command);
                return Results.NoContent();
            })
            .WithName(nameof(DeleteEventImagesBySomeEventEndpoint))
            .WithSummary("deletes all event images by SomeEvent ID")
            .WithDescription("deletes all event images associated with a specific SomeEvent")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .RequirePermission("Permissions.EventImages.Delete")
            .MapToApiVersion(1);
    }
}
