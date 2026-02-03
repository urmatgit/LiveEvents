using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Get.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.EventImages;

public static class GetAllEventImagesEndpoint
{
    internal static RouteHandlerBuilder MapGetAllEventImagesEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/", async (
                [AsParameters] GetAllEventImagesRequest request,
                ISender sender) =>
            {
                var result = await sender.Send(request);
                return Results.Ok(result);
            })
            .WithName(nameof(GetAllEventImagesEndpoint))
            .WithSummary("gets all event images")
            .WithDescription("gets all event images, optionally filtered by SomeEventId")
            .Produces<List<EventImageResponse>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .RequirePermission("Permissions.EventImages.View")
            .MapToApiVersion(1);
    }
}
