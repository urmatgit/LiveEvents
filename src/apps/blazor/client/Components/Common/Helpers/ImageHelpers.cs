using FSH.Starter.Blazor.Infrastructure.Api;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace FSH.Starter.Blazor.Client.Components.Common.Helpers;

public static class ImageHelpers
{
    public static async Task<FileUploadCommand?> ConvertToFileUploadCommandAsync(this IBrowserFile? browserFile, ISnackbar Toast,string namestart)
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
        string fileName = $"{namestart}-{Guid.NewGuid():N}";
        fileName = fileName[..Math.Min(fileName.Length, 90)];

        // Обработка изображения
        var imageFile = await browserFile.RequestImageFileAsync("image/jpeg", 800, 600);
        byte[] buffer = new byte[imageFile.Size];
        await imageFile.OpenReadStream(1024 * 1024 * 10).ReadAsync(buffer); // Максимальный размер 10MB
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
}
