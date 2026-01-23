using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Domain.Contracts;
using FSH.Starter.WebApi.Catalog.Domain.Contracts;
using FSH.Starter.WebApi.Catalog.Domain.Events;

namespace FSH.Starter.WebApi.Catalog.Domain;
public class SomeEvent : AuditableEntity, IAggregateWithName
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid OrganizationId { get; private set; }
    // min кол. участников
    public int MinParticipant { get; private set; }
    // max кол. участников
    public int MaxParticipant { get; private set; }
    //Min
    public int Durations { get; private set; }
    public double Price { get; private set; }
    // date and time
    public DateTime EventDate { get; private set; }
    
    // Связь с EventCatalog
    public Guid EventCatalogId { get; private set; }
    public virtual EventCatalog? EventCatalog { get; private set; }

    private SomeEvent() { }

    private SomeEvent(
        Guid id,
        string name,
        string description,
        Guid organizationId,
        int minParticipant,
        int maxParticipant,
        int durations,
        double price,
        DateTime eventDate,
        Guid eventCatalogId)
    {
        
        if (minParticipant > maxParticipant)
        {
            throw new ArgumentException("Минимальное количество участников не может превышать максимальное", nameof(minParticipant));
        }

        Id = id;
        Name = name;
        Description = description;
        OrganizationId = organizationId;
        MinParticipant = minParticipant;
        MaxParticipant = maxParticipant;
        Durations = durations;
        Price = price;
        EventDate = eventDate;
        EventCatalogId = eventCatalogId;
        
        QueueDomainEvent(new SomeEventCreated { SomeEvent = this });
    }

    public static SomeEvent Create(
        string name,
        string description,
        Guid organizationId,
        int minParticipant,
        int maxParticipant,
        int durations,
        double price,
        DateTime eventDate,
        Guid eventCatalogId)
    {
        
        if (minParticipant > maxParticipant)
        {
            throw new ArgumentException("Минимальное количество участников не может превышать максимальное", nameof(minParticipant));
        }

        var someEvent = new SomeEvent(
            Guid.NewGuid(),
            name,
            description,
            organizationId,
            minParticipant,
            maxParticipant,
            durations,
            price,
            eventDate,
            eventCatalogId);

        return someEvent;
    }

    public SomeEvent Update(
        string? name = null,
        string? description = null,
        Guid? organizationId = null,
        int? minParticipant = null,
        int? maxParticipant = null,
        int? durations = null,
        double? price = null,
        DateTime? eventDate = null,
        Guid? eventCatalogId = null)
    {
        bool isUpdated = false;

        if (!string.IsNullOrWhiteSpace(name) && !string.Equals(Name, name, StringComparison.OrdinalIgnoreCase))
        {
            Name = name;
            isUpdated = true;
        }

        if (description != null && !string.Equals(Description, description, StringComparison.OrdinalIgnoreCase))
        {
            Description = description;
            isUpdated = true;
        }

        if (organizationId.HasValue && OrganizationId != organizationId.Value)
        {
            OrganizationId = organizationId.Value;
            isUpdated = true;
        }

        if (minParticipant.HasValue)
        {
            
            // Проверяем, что максимальное количество участников не меньше минимального
            if (minParticipant.Value > MaxParticipant)
            {
                throw new ArgumentException("Минимальное количество участников не может превышать максимальное", nameof(minParticipant));
            }
            MinParticipant = minParticipant.Value;
            isUpdated = true;
        }

        if (maxParticipant.HasValue)
        {
            
            // Проверяем, что минимальное количество участников не больше максимального
            if (MinParticipant > maxParticipant.Value)
            {
                throw new ArgumentException("Минимальное количество участников не может превышать максимальное", nameof(maxParticipant));
            }
            MaxParticipant = maxParticipant.Value;
            isUpdated = true;
        }

        if (durations.HasValue && Durations != durations.Value)
        {
            Durations = durations.Value;
            isUpdated = true;
        }

        if (price.HasValue && Math.Abs(Price - price.Value) > 0.001) // Для сравнения double используем допустимую погрешность
        {
            Price = price.Value;
            isUpdated = true;
        }

        if (eventDate.HasValue && EventDate != eventDate.Value)
        {
            EventDate = eventDate.Value;
            isUpdated = true;
        }

        if (eventCatalogId.HasValue && EventCatalogId != eventCatalogId.Value)
        {
            EventCatalogId = eventCatalogId.Value;
            isUpdated = true;
        }

        if (isUpdated)
        {
            QueueDomainEvent(new SomeEventUpdated { SomeEvent = this });
        }

        return this;
    }
}
