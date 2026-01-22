using FSH.Framework.Core.Domain.Events;

namespace FSH.Starter.WebApi.Catalog.Domain.Events;
public sealed record EventCatalogUpdated : DomainEvent
{
    public EventCatalog? EventCatalog { get; set; }
}
