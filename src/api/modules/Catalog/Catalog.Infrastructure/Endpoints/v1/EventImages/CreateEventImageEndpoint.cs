using FSH.Framework.Core.Storage.File.Features;
using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.EventImages;

public static class CreateEventImageEndpoint
{
    internal static RouteHandlerBuilder MapCreateEventImageEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/", async (
                CreateEventImageCommand command,
                ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithName(nameof(CreateEventImageEndpoint))
            .WithSummary("creates a event image")
            .WithDescription("creates a event image")
            //.Accepts<CreateEventImageCommand>()
            .Produces<CreateEventImageResponse>()
            //.ProducesProblem(StatusCodes.Status400BadRequest)
            .RequirePermission("Permissions.EventImages.Create")
            .MapToApiVersion(1);
            //.WithOpenApi(x =>
            //{
            //    x.Summary = "Creates a new EventImage.";
            //    return x;
            //});
    }
}
