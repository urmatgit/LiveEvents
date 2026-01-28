using FSH.Starter.Blazor.Client.Components.EntityTable;
using FSH.Starter.Blazor.Infrastructure.Api;
using FSH.Starter.Shared.Authorization;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace FSH.Starter.Blazor.Client.Pages.Catalog;

public partial class SomeEvents
{
    [Inject]
    protected IApiClient _client { get; set; } = default!;

    protected EntityServerTableContext<SomeEventResponse, Guid, SomeEventViewModel> Context { get; set; } = default!;

    private EntityTable<SomeEventResponse, Guid, SomeEventViewModel> _table = default!;

    private List<EventCatalogResponse> _eventCatalogs = new();

    protected override async Task OnInitializedAsync()
    {
        Context = new(
            entityName: "SomeEvent",
            entityNamePlural: "SomeEvents",
            entityResource: FshResources.SomeEvents,
            fields: new()
            {
                new(field => field.Id,"Id", "Id"),
                new(field => field.Name,"Name", "Name"),
                new(field => field.Description, "Description", "Description"),
                new(field => field.Price, "Price", "Price"),
                new(field => field.EventDate, "EventDate", "EventDate"),
                new(field => field.EventCatalogId, "EventCatalogId", "EventCatalogId")
            },
            enableAdvancedSearch: true,
            idFunc: field => field.Id,
            searchFunc: async filter =>
            {
                var someEventFilter = filter.Adapt<SearchSomeEventsCommand>();
                someEventFilter.EventCatalogId = SearchEventCatalogId;
                var result = await _client.SearchSomeEventsEndpointAsync("1", someEventFilter);
                return result.Adapt<PaginationResponse<SomeEventResponse>>();
            },
            createFunc: async someEvent =>
            {
                await _client.CreateSomeEventEndpointAsync("1", someEvent.Adapt<CreateSomeEventCommand>());
            },
            updateFunc: async (id, someEvent) =>
            {
                await _client.UpdateSomeEventEndpointAsync("1", id, someEvent.Adapt<UpdateSomeEventCommand>());
            },
            deleteFunc: async id => await _client.DeleteSomeEventEndpointAsync("1", id));

        await LoadEventCatalogsAsync();
    }

    private async Task LoadEventCatalogsAsync()
    {
        if (_eventCatalogs.Count == 0)
        {
            var response = await _client.SearchEventCatalogsEndpointAsync("1", new SearchEventCatalogsCommand());
            if (response?.Items != null)
            {
                _eventCatalogs = response.Items.ToList();
            }
        }
    }

    // Advanced Search

    private Guid? _searchEventCatalogId;
    private Guid? SearchEventCatalogId
    {
        get => _searchEventCatalogId;
        set
        {
            _searchEventCatalogId = value;
            _ = _table.ReloadDataAsync();
        }
    }
}
