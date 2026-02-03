using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Create.v1;

public class CreateEventImageValidator : AbstractValidator<CreateEventImageCommand>
{
    public CreateEventImageValidator()
    {
        RuleFor(v => v.SomeEventId)
            .NotEmpty().WithMessage("SomeEventId is required.")
            .NotEqual(Guid.Empty).WithMessage("SomeEventId cannot be empty GUID.");

        RuleFor(v => v)
            .Must(HaveImageUrlOrImage)
            .WithMessage("Either ImageUrl or Image must be provided.");
            
        RuleFor(v => v.ImageUrl)
            .Must(BeAValidUrl).When(v => !string.IsNullOrEmpty(v.ImageUrl))
            .WithMessage("ImageUrl must be a valid URL.");
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
    
    private bool HaveImageUrlOrImage(CreateEventImageCommand command)
    {
        return !string.IsNullOrEmpty(command.ImageUrl) || command.Image != null;
    }
}
