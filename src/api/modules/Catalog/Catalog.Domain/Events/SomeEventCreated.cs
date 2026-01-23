using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Domain.Events;

namespace FSH.Starter.WebApi.Catalog.Domain.Events;

public record SomeEventCreated : DomainEvent
{
    public SomeEvent? SomeEvent { get; set; }
}
