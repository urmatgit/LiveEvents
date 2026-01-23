using Ardalis.Specification;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Contracts;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.GetByName;

public class GetSomeEventByNameSpecification : Specification<SomeEvent>
{
    public GetSomeEventByNameSpecification(string name)
    {
        Query.Where(p => p.Name.ToLower() == name.ToLower());
    }
    
    public GetSomeEventByNameSpecification(string name, Guid eventCatalogId)
    {
        Query.Where(p => p.Name.ToLower() == name.ToLower() && p.EventCatalogId == eventCatalogId);
    }
}
