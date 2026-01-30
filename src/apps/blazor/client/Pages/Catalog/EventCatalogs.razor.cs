using FSH.Starter.Blazor.Client.Components.EntityTable;
using FSH.Starter.Blazor.Infrastructure.Api;
using FSH.Starter.Shared.Authorization;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace FSH.Starter.Blazor.Client.Pages.Catalog;

public partial class EventCatalogs
{
    [Inject]
    protected IApiClient _client { get; set; } = default!;

    protected EntityServerTableContext<EventCatalogResponse, Guid, EventCatalogViewModel> Context { get; set; } = default!;

    private EntityTable<EventCatalogResponse, Guid, EventCatalogViewModel> _table = default!;

    protected override void OnInitialized() =>
    
        Context = new(
            entityName: "EventCatalog",
            entityNamePlural: "EventCatalogs",
            entityResource: FshResources.EventCatalogs,
            fields: new()
             {
              //   new(eventcatalog => eventcatalog.Id, "Id", "Id"),
                 new(eventcatalog => eventcatalog.Name, "Name", "Name"),
                 new(eventcatalog => eventcatalog.Description, "Description", "Description"),
                 new(eventcatalog => eventcatalog.ImageUrl, "Image", "ImageUrl")
             },
            enableAdvancedSearch: true,
            idFunc: eventcatalog => eventcatalog.Id!.Value,
            searchFunc: async filter =>
            {
                var eventCatalogFilter = filter.Adapt<SearchEventCatalogsCommand>();
                var result = await _client.SearchEventCatalogsEndpointAsync("1", eventCatalogFilter);
                return result.Adapt<PaginationResponse<EventCatalogResponse>>();
            },
            createFunc: async eventcatalog =>
            {
                var command = eventcatalog.Adapt<CreateEventCatalogCommand>();
                await _client.CreateEventCatalogEndpointAsync("1", command);
            },
            updateFunc: async (id, eventcatalog) =>
            {
                await _client.UpdateEventCatalogEndpointAsync("1", id,eventcatalog.Adapt<UpdateEventCatalogCommand>());
            },
            deleteFunc: async id => await _client.DeleteEventCatalogEndpointAsync("1",id));
    }


public class EventCatalogViewModel : UpdateEventCatalogCommand
{
    public EventCatalogViewModel() : base(default, default, default, default)
    {
    }
}
