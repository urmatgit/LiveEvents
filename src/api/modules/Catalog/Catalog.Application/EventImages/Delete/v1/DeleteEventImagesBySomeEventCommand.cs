using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Delete.v1;

public class DeleteEventImagesBySomeEventCommand : IRequest
{
    public Guid SomeEventId { get; set; }
    
    public DeleteEventImagesBySomeEventCommand(Guid someEventId)
    {
        SomeEventId = someEventId;
    }
}
