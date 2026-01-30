using FSH.Starter.Blazor.Client.Components.Dialogs;
using FSH.Starter.Blazor.Client.Components.EntityTable;
using FSH.Starter.Blazor.Infrastructure.Api;
using FSH.Starter.Shared.Authorization;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

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
                 new(eventcatalog => eventcatalog.ImageUrl, "Image", "ImageUrl",Template: ImageFieldTemplate)
               
                
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
                // Преобразуем IBrowserFile в FileUploadCommand если есть изображение
                var imageCommand = await ConvertToFileUploadCommandAsync(eventcatalog.ImageFile);
            var command = new CreateEventCatalogCommand();
            command.Name = eventcatalog.Name;
            command.Description = eventcatalog.Description;
            command.Image = imageCommand;
                
                await _client.CreateEventCatalogEndpointAsync("1", command);
                
                // Обновляем информацию о сущности после создания
                StateHasChanged();
            },
            updateFunc: async (id, eventcatalog) =>
            {
                // Преобразуем IBrowserFile в FileUploadCommand если есть изображение
                var imageCommand = await ConvertToFileUploadCommandAsync(eventcatalog.ImageFile);
            var command = new UpdateEventCatalogCommand();
            command.Id = id;
            command.Name = eventcatalog.Name;
            command.Description = eventcatalog.Description;
            command.Image = imageCommand;
            command.DeleteCurrentImage = eventcatalog.DeleteCurrentImage;
                    
                await _client.UpdateEventCatalogEndpointAsync("1", id, command);
                
                // Обновляем информацию о сущности после обновления
                StateHasChanged();
            },
            deleteFunc: async id => await _client.DeleteEventCatalogEndpointAsync("1",id));

    private async Task<FileUploadCommand?> ConvertToFileUploadCommandAsync(IBrowserFile? browserFile)
    {
        if (browserFile == null) return null;

        // Проверяем формат изображения
        string? extension = Path.GetExtension(browserFile.Name);
        if (!IsSupportedImageFormat(extension))
        {
            Toast.Add("Image Format Not Supported.", Severity.Error);
            return null;
        }

        // Генерируем уникальное имя файла
        string fileName = $"eventcatalog-{Guid.NewGuid():N}";
        fileName = fileName[..Math.Min(fileName.Length, 90)];

        // Обработка изображения
        var imageFile = await browserFile.RequestImageFileAsync("image/jpeg", 800, 600);
        byte[] buffer = new byte[imageFile.Size];
        await imageFile.OpenReadStream(1024*1024*10).ReadAsync(buffer); // Максимальный размер 10MB
        string base64String = $"data:image/jpeg;base64,{Convert.ToBase64String(buffer)}";

        return new FileUploadCommand
        {
            Name = fileName,
            Data = base64String,
            Extension = extension
        };
    }

    private static bool IsSupportedImageFormat(string? extension)
    {
        var supportedFormats = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        return extension != null && supportedFormats.Contains(extension.ToLower());
    }
    public async Task RemoveImageAsync()
    {
        string deleteContent = "You're sure you want to delete your Profile Image?";
        var parameters = new DialogParameters
        {
            { nameof(DeleteConfirmation.ContentText), deleteContent }
        };
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, BackdropClick = false };
        var dialog = await DialogService.ShowAsync<DeleteConfirmation>("Delete", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            Context.AddEditModal.RequestModel.DeleteCurrentImage = true;
            Context.AddEditModal.RequestModel.ImageUrl = "";
            Context.AddEditModal.RequestModel.ImageFile = null;
            StateHasChanged();
            
            Context.AddEditModal.ForceRender();
        }
    }
}


public class EventCatalogViewModel : UpdateEventCatalogCommand
{
    
    
    // Свойства для работы с файлами в Blazor клиенте
    public IBrowserFile? ImageFile { get; set; }
    public string ImageUrl { get; set; }


}
