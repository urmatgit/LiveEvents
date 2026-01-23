using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Domain.Contracts;
using FSH.Starter.WebApi.Catalog.Domain.Contracts;
using FSH.Starter.WebApi.Catalog.Domain.Events;

namespace FSH.Starter.WebApi.Catalog.Domain;
public class EventCatalog : AuditableEntity, IAggregateWithName
{
    public string Name { get;  set; } = string.Empty;
    public string? Description { get; private set; }

    private EventCatalog() { }

    private EventCatalog(Guid id, string name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
        QueueDomainEvent(new EventCatalogCreated { EventCatalog = this });
    }

    public static EventCatalog Create(string name, string? description)
    {
        return new EventCatalog(Guid.NewGuid(), name, description);
    }

    public EventCatalog Update(string? name, string? description)
    {
        bool isUpdated = false;

        if (!string.IsNullOrWhiteSpace(name) && !string.Equals(Name, name, StringComparison.OrdinalIgnoreCase))
        {
            Name = name;
            isUpdated = true;
        }

        if (!string.Equals(Description, description, StringComparison.OrdinalIgnoreCase))
        {
            Description = description;
            isUpdated = true;
        }

        if (isUpdated)
        {
            QueueDomainEvent(new EventCatalogUpdated { EventCatalog = this });
        }

        return this;
    }
}
