using FSH.Framework.Core.Exceptions;

namespace FSH.Starter.WebApi.Catalog.Domain.Exceptions;

public sealed class EventCatalogNotFoundException : NotFoundException
{
    public EventCatalogNotFoundException(Guid id) : base($"EventCatalog {id} was not found.")
    {
    }
}
