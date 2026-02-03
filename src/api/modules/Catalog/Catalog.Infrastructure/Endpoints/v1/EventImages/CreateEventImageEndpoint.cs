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
                HttpRequest request,
                ISender sender) =>
            {
                // Parse form data manually to handle both URL and file upload
                var form = await request.ReadFormAsync();
                
                string? imageUrl = form["ImageUrl"].ToString();
                Guid someEventId = Guid.Parse(form["SomeEventId"].ToString() ?? Guid.Empty.ToString());
                
                FileUploadCommand? image = null;
                if (form.Files.Count > 0 && form.Files["Image"] != null)
                {
                    var file = form.Files["Image"];
                    if (file.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        
                        image = new FileUploadCommand
                        {
                            Name = file.FileName,
                            Extension = Path.GetExtension(file.FileName),
                            Data = Convert.ToBase64String(fileBytes)
                        };
                    }
                }
                
                var command = new CreateEventImageCommand(imageUrl, someEventId, image);
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithName(nameof(CreateEventImageEndpoint))
            .WithSummary("creates a event image")
            .WithDescription("creates a event image")
            .Accepts<CreateEventImageCommand>(contentType: "multipart/form-data")
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
