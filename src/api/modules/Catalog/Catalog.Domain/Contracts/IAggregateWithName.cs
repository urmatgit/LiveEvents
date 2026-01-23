using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSH.Framework.Core.Domain.Contracts;

namespace FSH.Starter.WebApi.Catalog.Domain.Contracts;
public interface IAggregateWithName: IAggregateRoot
{
    string Name { get; set; }
}
