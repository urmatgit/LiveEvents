using System.ComponentModel;
using FSH.Framework.Core.Storage.File.Features;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Create.v1;

public sealed record CreateEventCatalogCommand(
    [property: DefaultValue("Sample EventCatalog")] string? Name,
    [property: DefaultValue("Descriptive Description")] string? Description = null,
    FileUploadCommand? Image = null) : IRequest<CreateEventCatalogResponse>;
