using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Create.v1;

public sealed class CreateSomeEventHandler(
    [FromKeyedServices("catalog:someevents")] IRepository<SomeEvent> repository,
    ILogger<CreateSomeEventHandler> logger)
    : IRequestHandler<CreateSomeEventCommand, CreateSomeEventResponse>
{
    public async Task<CreateSomeEventResponse> Handle(CreateSomeEventCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Creating SomeEvent with Name: {Name}, OrganizationId: {OrganizationId}", request.Name, request.OrganizationId);

        var someEvent = SomeEvent.Create(
            request.Name,
            request.Description,
            request.OrganizationId,
            request.MinParticipant,
            request.MaxParticipant,
            request.Durations,
            request.Price,
            request.EventDate,
            request.EventCatalogId);

        await repository.AddAsync(someEvent, cancellationToken);

        logger.LogInformation("Successfully created SomeEvent with ID: {SomeEventId}", someEvent.Id);

        return new CreateSomeEventResponse(someEvent.Id);
    }
}
