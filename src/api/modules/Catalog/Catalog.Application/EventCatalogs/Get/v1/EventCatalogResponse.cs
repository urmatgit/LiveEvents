namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Get.v1;

public sealed record EventCatalogResponse(Guid? Id, string Name, string? Description);
