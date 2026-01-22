using System.ComponentModel;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Update.v1;

public sealed record UpdateEventCatalogCommand(
    Guid Id,
    string? Name,
    string? Description = null) : IRequest<UpdateEventCatalogResponse>;
