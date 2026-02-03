using FSH.Framework.Core.Domain;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Framework.Core.Domain.Contracts;

namespace FSH.Starter.WebApi.Catalog.Domain;

public class EventImage : BaseEntity, IAggregateRoot
{
    public Uri? ImageUrl { get; private set; }
    public Guid SomeEventId { get; private set; }
    public virtual SomeEvent? SomeEvent { get; private set; }

    private EventImage()
    {
        // Parameterless constructor for EF Core
    }

    private EventImage(Guid id, Uri imageUrl, Guid someEventId)
    {
        Id = id;
        ImageUrl = imageUrl;
        SomeEventId = someEventId;
    }

    public static EventImage Create(Uri imageUrl, Guid someEventId)
    {
        var eventImage = new EventImage(
            Guid.NewGuid(),
            imageUrl,
            someEventId);

        return eventImage;
    }

    public EventImage Update(Uri? imageUrl = null, Guid? someEventId = null)
    {
        bool isUpdated = false;

        if (imageUrl != null && ImageUrl != imageUrl)
        {
            ImageUrl = imageUrl;
            isUpdated = true;
        }

        if (someEventId.HasValue && SomeEventId != someEventId.Value)
        {
            SomeEventId = someEventId.Value;
            isUpdated = true;
        }

        return this;
    }
}
