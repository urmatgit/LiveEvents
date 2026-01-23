using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Delete.v1;

public sealed class DeleteSomeEventHandler(
    [FromKeyedServices("catalog:someevents")] IRepository<SomeEvent> repository,
    ILogger<DeleteSomeEventHandler> logger)
    : IRequestHandler<DeleteSomeEventCommand, DeleteSomeEventResponse>
{
    public async Task<DeleteSomeEventResponse> Handle(DeleteSomeEventCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Deleting SomeEvent with ID: {SomeEventId}", request.Id);

        var someEvent = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (someEvent == null)
        {
            logger.LogWarning("SomeEvent with ID {SomeEventId} not found for deletion", request.Id);
            throw new SomeEventNotFoundException(request.Id);
        }

        await repository.DeleteAsync(someEvent, cancellationToken);

        logger.LogInformation("Successfully deleted SomeEvent with ID: {SomeEventId}", someEvent.Id);

        return new DeleteSomeEventResponse(someEvent.Id);
    }
}
