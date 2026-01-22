using FSH.Framework.Core.Domain.Events;

namespace FSH.Starter.WebApi.Catalog.Domain.Events;
public sealed record EventCatalogCreated : DomainEvent
{
    public EventCatalog? EventCatalog { get; set; }
}
