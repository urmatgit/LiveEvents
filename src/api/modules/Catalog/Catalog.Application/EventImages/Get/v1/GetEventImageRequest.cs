using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Get.v1;

public class GetEventImageRequest : IRequest<EventImageResponse>
{
    public Guid Id { get; set; }
    
    public GetEventImageRequest(Guid id)
    {
        Id = id;
    }
}
