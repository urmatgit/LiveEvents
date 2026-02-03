using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Get.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.EventImages;

public static class GetEventImageEndpoint
{
    internal static RouteHandlerBuilder MapGetEventImageEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/{id:guid}", async (
                [AsParameters] GetEventImageRequest request,
                ISender sender) =>
            {
                var result = await sender.Send(request);
                return Results.Ok(result);
            })
            .WithName(nameof(GetEventImageEndpoint))
            .WithSummary("gets a event image by id")
            .WithDescription("gets a event image by id")
            .Produces<EventImageResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .RequirePermission("Permissions.EventImages.View")
            .MapToApiVersion(1);
    }
}
