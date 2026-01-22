using System.ComponentModel;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Create.v1;

public sealed record CreateEventCatalogCommand(
    [property: DefaultValue("Sample EventCatalog")] string? Name,
    [property: DefaultValue("Descriptive Description")] string? Description = null) : IRequest<CreateEventCatalogResponse>;
