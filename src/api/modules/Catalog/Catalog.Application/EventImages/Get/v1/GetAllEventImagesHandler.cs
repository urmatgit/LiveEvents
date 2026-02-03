using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Get.v1;

public sealed class GetAllEventImagesHandler(
    [FromKeyedServices("catalog:eventimages")] IReadRepository<EventImage> repository,
    ILogger<GetAllEventImagesHandler> logger)
    : IRequestHandler<GetAllEventImagesRequest, List<EventImageResponse>>
{
    public async Task<List<EventImageResponse>> Handle(GetAllEventImagesRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all EventImages");

        List<EventImage> eventImages;
        
        if (request.SomeEventId.HasValue)
        {
            // Filter by SomeEventId if provided
            var spec = new GetEventImagesBySomeEventIdSpecification(request.SomeEventId.Value);
            eventImages = await repository.ListAsync(spec, cancellationToken);
        }
        else
        {
            // Get all event images
            eventImages = await repository.ListAsync(cancellationToken);
        }

        logger.LogInformation("Successfully retrieved {Count} EventImages", eventImages.Count);

        return eventImages.Select(EventImageResponse.FromEntity).ToList();
    }
}
