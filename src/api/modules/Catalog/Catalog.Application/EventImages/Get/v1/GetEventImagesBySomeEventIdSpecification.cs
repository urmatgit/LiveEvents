using Ardalis.Specification;
using FSH.Starter.WebApi.Catalog.Domain;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Get.v1;

public class GetEventImagesBySomeEventIdSpecification : Specification<EventImage>
{
    public GetEventImagesBySomeEventIdSpecification(Guid someEventId)
    {
        Query.Where(ei => ei.SomeEventId == someEventId);
    }
}
