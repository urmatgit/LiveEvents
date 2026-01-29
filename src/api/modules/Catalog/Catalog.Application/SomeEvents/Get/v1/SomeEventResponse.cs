namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Get.v1;

public record SomeEventResponse(
    Guid Id,
    string Name,
    string Description,
    Guid OrganizationId,
    int MinParticipant,
    int MaxParticipant,
    int Durations,
    double Price,
    DateTime EventDate,
    Guid EventCatalogId,
    string EventCatalogName);
