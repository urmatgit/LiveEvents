using FSH.Framework.Core.Exceptions;

namespace FSH.Starter.WebApi.Catalog.Domain.Exceptions;

public class SomeEventNotFoundException : NotFoundException
{
    public SomeEventNotFoundException(Guid id) 
        : base($"SomeEvent {id} not found.") { }

    public SomeEventNotFoundException(string name) 
        : base($"SomeEvent '{name}' not found.") { }
}
