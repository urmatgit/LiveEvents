using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Update.v1;

public sealed class UpdateSomeEventHandler(
    [FromKeyedServices("catalog:someevents")] IRepository<SomeEvent> repository,
    ILogger<UpdateSomeEventHandler> logger)
    : IRequestHandler<UpdateSomeEventCommand, UpdateSomeEventResponse>
{
    public async Task<UpdateSomeEventResponse> Handle(UpdateSomeEventCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Updating SomeEvent with ID: {SomeEventId}", request.Id);

        var someEvent = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (someEvent == null)
        {
            logger.LogWarning("SomeEvent with ID {SomeEventId} not found", request.Id);
            throw new SomeEventNotFoundException(request.Id);
        }

        someEvent.Update(
            request.Name,
            request.Description,
            request.OrganizationId,
            request.MinParticipant,
            request.MaxParticipant,
            request.Durations,
            request.Price,
            request.EventDate,
            request.EventCatalogId);

        await repository.UpdateAsync(someEvent, cancellationToken);

        logger.LogInformation("Successfully updated SomeEvent with ID: {SomeEventId}", someEvent.Id);

        return new UpdateSomeEventResponse(someEvent.Id);
    }
}
