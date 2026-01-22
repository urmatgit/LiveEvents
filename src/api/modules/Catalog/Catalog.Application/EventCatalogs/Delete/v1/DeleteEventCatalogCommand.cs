using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Delete.v1;

public sealed record DeleteEventCatalogCommand(
    Guid Id) : IRequest;
