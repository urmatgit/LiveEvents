using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.SomeEvents.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.SomeEvents;

public static class CreateSomeEventEndpoint
{
    internal static RouteHandlerBuilder MapSomeEventCreationEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/", async (
                CreateSomeEventCommand command,
                ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithName(nameof(CreateSomeEventEndpoint))
            .WithSummary("creates a some event")
            .WithDescription("creates a some event")
            .Produces<CreateSomeEventResponse>()
            //.ProducesProblem(StatusCodes.Status400BadRequest)
            .RequirePermission("Permissions.SomeEvents.Create")
            .MapToApiVersion(1);
            //.WithOpenApi(x =>
            //{
            //    x.Summary = "Creates a new SomeEvent.";
            //    return x;
            //});
    }
}
