using FSH.Framework.Core.Exceptions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Storage;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using FSH.Framework.Core.Storage.File;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Update.v1;

public sealed class UpdateEventCatalogHandler(
    ILogger<UpdateEventCatalogHandler> logger,
    [FromKeyedServices("catalog:eventcatalogs")] IRepository<EventCatalog> repository,
    IStorageService storageService)
    : IRequestHandler<UpdateEventCatalogCommand, UpdateEventCatalogResponse>
{
    public async Task<UpdateEventCatalogResponse> Handle(UpdateEventCatalogCommand request, CancellationToken cancellationToken)
    {
        var eventCatalog = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = eventCatalog ?? throw new NotFoundException($"EventCatalog with id : {request.Id} not found.");
        
        Uri? currentImageUrl = eventCatalog.ImageUrl;
        Uri? newImageUrl = currentImageUrl;
        
        if (request.Image != null || request.DeleteCurrentImage)
        {
            if (request.Image != null)
            {
                newImageUrl = await storageService.UploadAsync<EventCatalog>(request.Image, FileType.Image, cancellationToken);
            }
            else if (request.DeleteCurrentImage && currentImageUrl != null)
            {
                storageService.Remove(currentImageUrl);
                newImageUrl = null;
            }
        }
        
        var updatedEventCatalog = eventCatalog.Update(request.Name, request.Description, newImageUrl);
        await repository.UpdateAsync(updatedEventCatalog, cancellationToken);
        logger.LogInformation("EventCatalog with id : {EventCatalogId} updated.", eventCatalog.Id);
        return new UpdateEventCatalogResponse(eventCatalog.Id);
    }
}
