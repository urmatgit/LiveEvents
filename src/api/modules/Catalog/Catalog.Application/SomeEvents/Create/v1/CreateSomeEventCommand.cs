using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Create.v1;

public class CreateSomeEventCommand : IRequest<CreateSomeEventResponse>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid OrganizationId { get; set; }
    public int MinParticipant { get; set; } = 1;
    public int MaxParticipant { get; set; }
    public int Durations { get; set; } = 60;
    public double Price { get; set; }
    public DateTime EventDate { get; set; }
    public Guid EventCatalogId { get; set; }
}
