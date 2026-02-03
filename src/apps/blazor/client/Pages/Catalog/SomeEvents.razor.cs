using FSH.Starter.Blazor.Client.Components.EntityTable;
using FSH.Starter.Blazor.Infrastructure.Api;
using FSH.Starter.Shared.Authorization;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace FSH.Starter.Blazor.Client.Pages.Catalog;

public partial class SomeEvents
{
    [Inject]
    protected IApiClient _client { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private IStringLocalizer<SomeEvents> L { get; set; } = default!;

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
                new(field => field.OrganizationName,"Organization", "Organization"),
                new(field => field.Description, "Description", "Description"),
                new(field => field.Price, "Price", "Price"),
                new(field => field.EventDate, "EventDate", "EventDate"),
                new(field => field.EventCatalogName, "EventCatalogName", "EventCatalogName")
            },
            enableAdvancedSearch: true,
            idFunc: field => field.Id,
            searchFunc: async filter =>
            {
                var someEventFilter = filter.Adapt<SearchSomeEventsCommand>();
                someEventFilter.EventCatalogId = SearchEventCatalogId;
                var result = await _client.SearchSomeEventsEndpointAsync("1", someEventFilter);
                var paginatedResult = result.Adapt<PaginationResponse<SomeEventResponse>>();
                
                // Преобразуем результат, чтобы разделить дату и время для отображения
                foreach (var item in paginatedResult.Items)
                {
                    var viewModel = item.Adapt<SomeEventViewModel>();
                    viewModel.LocalEventDateTime = item.EventDate;
                }
                
                return paginatedResult;
            },
            createFunc: async someEvent =>
            {
                // The LocalEventDateTime property setter handles the UTC conversion automatically
                await _client.CreateSomeEventEndpointAsync("1", someEvent.Adapt<CreateSomeEventCommand>());
            },
            updateFunc: async (id, someEvent) =>
            {
                // The LocalEventDateTime property setter handles the UTC conversion automatically
                await _client.UpdateSomeEventEndpointAsync("1", id, someEvent.Adapt<UpdateSomeEventCommand>());
            },
            deleteFunc: async id => await _client.DeleteSomeEventEndpointAsync("1", id));

        await LoadEventCatalogsAsync();
    }

    private async Task LoadEventCatalogsAsync()
    {
        if (_eventCatalogs.Count == 0)
        {
            var response = await _client.GetAllEventCatalogsEndpointAsync("1" );
            if (response != null)
            {
                _eventCatalogs = response.ToList();
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

    public async Task ViewImages(SomeEventResponse someEvent)
    {
        var options = new DialogOptions() { MaxWidth = MaxWidth.Large, FullWidth = true, CloseButton = true, DisableBackdropClick = true };
        var parameters = new DialogParameters()
        {
            { nameof(EventImageViewerModal.SomeEventId), someEvent.Id }
        };

        var dialog = DialogService.Show<EventImageViewerModal>(L["Images"], parameters, options);
        var result = await dialog.Result;
    }
}
