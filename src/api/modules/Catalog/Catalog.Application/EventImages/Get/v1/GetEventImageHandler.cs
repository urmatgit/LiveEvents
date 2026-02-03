using FSH.Framework.Core.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Get.v1;

public sealed class GetEventImageHandler(
    [FromKeyedServices("catalog:eventimages")] IReadRepository<EventImage> repository,
    ILogger<GetEventImageHandler> logger)
    : IRequestHandler<GetEventImageRequest, EventImageResponse>
{
    public async Task<EventImageResponse> Handle(GetEventImageRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting EventImage with ID: {EventImageId}", request.Id);

        var eventImage = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (eventImage == null)
        {
            throw new NotFoundException($"EventImage with ID {request.Id} not found.");
        }

        logger.LogInformation("Successfully retrieved EventImage with ID: {EventImageId}", request.Id);

        return EventImageResponse.FromEntity(eventImage);
    }
}
