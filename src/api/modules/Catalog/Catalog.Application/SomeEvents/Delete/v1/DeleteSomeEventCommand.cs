using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Delete.v1;

public class DeleteSomeEventCommand : IRequest<DeleteSomeEventResponse>
{
    public Guid Id { get; set; }
    
    public DeleteSomeEventCommand(Guid id)
    {
        Id = id;
    }
}
