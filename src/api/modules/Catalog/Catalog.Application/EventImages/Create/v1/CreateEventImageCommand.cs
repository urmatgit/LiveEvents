using MediatR;
using FSH.Starter.WebApi.Catalog.Application.EventImages.Create.v1;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Create.v1;

public sealed record CreateEventImageCommand(
    string? ImageUrl = null,
    Guid SomeEventId = default,
    FSH.Framework.Core.Storage.File.Features.FileUploadCommand? Image = null) : IRequest<CreateEventImageResponse>;
