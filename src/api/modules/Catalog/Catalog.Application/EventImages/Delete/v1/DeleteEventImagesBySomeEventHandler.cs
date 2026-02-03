using FSH.Framework.Core.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Delete.v1;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Delete.v1;

public sealed class DeleteEventImagesBySomeEventHandler(
    [FromKeyedServices("catalog:eventimages")] IRepository<EventImage> repository,
    ILogger<DeleteEventImagesBySomeEventHandler> logger)
    : IRequestHandler<DeleteEventImagesBySomeEventCommand>
{
    public async Task Handle(DeleteEventImagesBySomeEventCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting all EventImages for SomeEvent with ID: {SomeEventId}", request.SomeEventId);

        // Get all event images for the specified SomeEvent
        var spec = new GetEventImagesBySomeEventIdSpecification(request.SomeEventId);
        var eventImages = await repository.ListAsync(spec, cancellationToken);

        if (eventImages == null || eventImages.Count == 0)
        {
            logger.LogInformation("No EventImages found for SomeEvent with ID: {SomeEventId}", request.SomeEventId);
         
        }

        // Delete all event images
        foreach (var eventImage in eventImages)
        {
            await repository.DeleteAsync(eventImage, cancellationToken);
        }

        logger.LogInformation("Successfully deleted {Count} EventImages for SomeEvent with ID: {SomeEventId}", 
            eventImages.Count, request.SomeEventId);

        
    }
}
