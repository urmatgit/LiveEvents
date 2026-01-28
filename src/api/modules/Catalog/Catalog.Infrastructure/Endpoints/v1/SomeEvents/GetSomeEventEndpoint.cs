using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.SomeEvents.Get.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.SomeEvents;

public static class GetSomeEventEndpoint
{
    internal static RouteHandlerBuilder MapGetSomeEventEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/{id:guid}", async (
                Guid id,
                ISender sender) =>
            {
                var result = await sender.Send(new GetSomeEventRequest(id));
                return Results.Ok(result);
            })
            .WithName(nameof(GetSomeEventEndpoint))
            .WithSummary("Gets a SomeEvent by id.")
            .WithDescription("Gets a SomeEvent by id.")
            .Produces<SomeEventResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .RequirePermission("Permissions.SomeEvents.View")
            .MapToApiVersion(1);
    }
}
