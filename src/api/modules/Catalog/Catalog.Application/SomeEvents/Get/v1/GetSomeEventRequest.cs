using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Get.v1;

public class GetSomeEventRequest : IRequest<SomeEventResponse>
{
    public Guid Id { get; set; }
    
    public GetSomeEventRequest(Guid id)
    {
        Id = id;
    }
}
