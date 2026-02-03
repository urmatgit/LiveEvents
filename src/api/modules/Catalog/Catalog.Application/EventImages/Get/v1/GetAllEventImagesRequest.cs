using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Get.v1;

public class GetAllEventImagesRequest : IRequest<List<EventImageResponse>>
{
    public Guid? SomeEventId { get; set; }
    
    public GetAllEventImagesRequest(Guid? someEventId = null)
    {
        SomeEventId = someEventId;
    }
}
