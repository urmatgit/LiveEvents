using MediatR;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Create.v1;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Create.v1;

public class CreateEventImageCommand : IRequest<CreateEventImageResponse>
{
    public string? ImageUrl { get; set; }
    public Guid SomeEventId { get; set; }
}
