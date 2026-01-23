using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain.Events;

namespace FSH.Starter.WebApi.Catalog.Domain.Events;

public record SomeEventUpdated : DomainEvent
{
    public SomeEvent? SomeEvent { get; set; } = default!;
}
