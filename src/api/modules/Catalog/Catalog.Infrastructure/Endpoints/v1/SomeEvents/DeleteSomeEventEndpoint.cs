using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.SomeEvents.Delete.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.SomeEvents;

public static class DeleteSomeEventEndpoint
{
    internal static RouteHandlerBuilder MapSomeEventDeleteEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapDelete("/{id:guid}", async (
                Guid id,
                ISender sender) =>
            {
                var result = await sender.Send(new DeleteSomeEventCommand(id));
                return Results.Ok(result);
            })
            .WithName(nameof(DeleteSomeEventEndpoint))
            .WithSummary("deletes a some event by id")
            .WithDescription("deletes a some event by id")
            .Produces<DeleteSomeEventResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .RequirePermission("Permissions.SomeEvents.Delete")
            .MapToApiVersion(1);
    }
}
