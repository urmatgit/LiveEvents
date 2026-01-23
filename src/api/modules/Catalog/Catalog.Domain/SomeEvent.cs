using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSH.Framework.Core.Domain;
using FSH.Starter.WebApi.Catalog.Domain.Contracts;

namespace FSH.Starter.WebApi.Catalog.Domain;
public class SomeEvent : AuditableEntity, IAggregateWithName
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid OrganizationId { get; private set; }
    // min кол. участников
    public int MinParticipant { get; private set; }
    // max кол. участников
    public int MaxParticipant { get; private set; }
    //Min
    public int Durations { get;  private set; }
    public double Price { get; private set; }
    // date and time
    public DateTime EventDate { get; private set; }

}
