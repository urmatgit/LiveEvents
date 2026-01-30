using System.ComponentModel;
using FSH.Framework.Core.Storage.File.Features;
using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Update.v1;

public sealed record UpdateEventCatalogCommand(
    Guid Id,
    string? Name,
    string? Description = null,
    FileUploadCommand? Image = null,
    bool DeleteCurrentImage = false) : IRequest<UpdateEventCatalogResponse>;
