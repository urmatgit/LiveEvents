using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Update.v1;

public class UpdateSomeEventCommand : IRequest<UpdateSomeEventResponse>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid? OrganizationId { get; set; }
    public int? MinParticipant { get; set; }
    public int? MaxParticipant { get; set; }
    public int? Durations { get; set; }
    public double? Price { get; set; }
    public DateTime? EventDate { get; set; }
    public Guid? EventCatalogId { get; set; }
}
