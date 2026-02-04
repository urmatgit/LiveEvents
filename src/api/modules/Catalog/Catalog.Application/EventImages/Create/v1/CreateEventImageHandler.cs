using FSH.Framework.Core.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Storage;
using FSH.Framework.Core.Storage.File;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Create.v1;

public sealed class CreateEventImageHandler(
    [FromKeyedServices("catalog:eventimages")] IRepository<EventImage> repository,
    [FromKeyedServices("catalog:someevents")] IRepository<SomeEvent> someEventRepository,
    ILogger<CreateEventImageHandler> logger,
    IStorageService storageService)
    : IRequestHandler<CreateEventImageCommand, CreateEventImageResponse>
{
    public async Task<CreateEventImageResponse> Handle(CreateEventImageCommand request, CancellationToken cancellationToken)
    {
        // Validate the SomeEvent exists
        var someEvent = await someEventRepository.GetByIdAsync(request.SomeEventId, cancellationToken);
        
        if (someEvent == null)
        {
            throw new NotFoundException($"SomeEvent with ID {request.SomeEventId} not found.");
        }

        // Create the EventImage entity
        Uri? imageUrl = null;
        if (request.Image != null)
        {
            imageUrl = await storageService.UploadAsync<EventImage>(request.Image, FileType.Image, cancellationToken);
        }
        else if (!string.IsNullOrEmpty(request.ImageUrl))
        {
            if (!Uri.TryCreate(request.ImageUrl, UriKind.Absolute, out imageUrl))
            {
                throw new ValidationException("Invalid URL format for ImageUrl");
            }
        }

        var eventImage = EventImage.Create(imageUrl, request.SomeEventId);

        // Add to repository
        await repository.AddAsync(eventImage, cancellationToken);

        logger.LogInformation("Successfully created EventImage with ID: {EventImageId}", eventImage.Id);

        return new CreateEventImageResponse { Id = eventImage.Id };
    }
}
