using Ardalis.Specification;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.GetByName;

public class GetSomeEventByNameRequest : IRequest<SomeEvent>
{
    public string Name { get; set; }
    public Guid? EventCatalogId { get; set; }
    
    public GetSomeEventByNameRequest(string name, Guid? eventCatalogId = null)
    {
        Name = name;
        EventCatalogId = eventCatalogId;
    }
}

public sealed class GetSomeEventByNameHandler(
    [FromKeyedServices("catalog:someevents")] IReadRepository<SomeEvent> repository)
    : IRequestHandler<GetSomeEventByNameRequest, SomeEvent>
{
    public async Task<SomeEvent> Handle(GetSomeEventByNameRequest request, CancellationToken cancellationToken)
    {
        ISpecification<SomeEvent> specification;
        
        if (request.EventCatalogId.HasValue)
        {
            specification = new GetSomeEventByNameSpecification(request.Name, request.EventCatalogId.Value);
        }
        else
        {
            specification = new GetSomeEventByNameSpecification(request.Name);
        }
        
        var result = await repository.ListAsync(specification, cancellationToken);
        
        return result.FirstOrDefault() ?? throw new SomeEventNotFoundException(request.Name);
    }
}
