using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Get.v1;

public class EventImageResponse
{
    public Guid Id { get; set; }
    public string? ImageUrl { get; set; }
    public Guid SomeEventId { get; set; }
    
    public static EventImageResponse FromEntity(EventImage entity)
    {
        return new EventImageResponse
        {
            Id = entity.Id,
            ImageUrl = entity.ImageUrl?.ToString(),
            SomeEventId = entity.SomeEventId
        };
    }
}
