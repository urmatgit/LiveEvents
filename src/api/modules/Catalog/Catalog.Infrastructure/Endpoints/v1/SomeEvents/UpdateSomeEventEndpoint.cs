using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.SomeEvents.Update.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.SomeEvents;

public static class UpdateSomeEventEndpoint
{
    internal static RouteHandlerBuilder MapSomeEventUpdateEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapPut("/{id:guid}", async (
                Guid id,
                UpdateSomeEventCommand command,
                ISender sender) =>
            {
                command.Id = id;
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithName(nameof(UpdateSomeEventEndpoint))
            .WithSummary("update a SomeEvent")
            .WithDescription("update a SomeEvent")
            .Produces<UpdateSomeEventResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .RequirePermission("Permissions.SomeEvents.Update")
            .MapToApiVersion(1);
    }
}
