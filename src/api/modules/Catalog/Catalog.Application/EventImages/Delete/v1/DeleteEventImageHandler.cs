using FSH.Framework.Core.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Delete.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Delete.v1;

public sealed class DeleteEventImageHandler(
    [FromKeyedServices("catalog:eventimages")] IRepository<EventImage> repository,
    ILogger<DeleteEventImageHandler> logger)
    : IRequestHandler<DeleteEventImageCommand>
{
    public async Task Handle(DeleteEventImageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting EventImage with ID: {EventImageId}", request.Id);

        var eventImage = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (eventImage == null)
        {
            throw new NotFoundException($"EventImage with ID {request.Id} not found.");
        }

        await repository.DeleteAsync(eventImage, cancellationToken);

        logger.LogInformation("Successfully deleted EventImage with ID: {EventImageId}", request.Id);

        
    }
}
