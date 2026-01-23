using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using FSH.Framework.Core.Domain;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Contracts;

namespace FSH.Starter.WebApi.Catalog.Application.Specifications;
public class GetByNameSpecification<T>:Specification<T> where T : IAggregateWithName
{
    public GetByNameSpecification(string name)
    {
        Query.Where(p=>p.Name.ToLower()== name.ToLower()); 
    }
}
