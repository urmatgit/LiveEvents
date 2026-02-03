using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Delete.v1;

public class DeleteEventImageCommand : IRequest
{
    public Guid Id { get; set; }
    
    public DeleteEventImageCommand(Guid id)
    {
        Id = id;
    }
}
