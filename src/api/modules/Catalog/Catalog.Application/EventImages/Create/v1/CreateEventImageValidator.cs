using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Create.v1;

public class CreateEventImageValidator : AbstractValidator<CreateEventImageCommand>
{
    public CreateEventImageValidator()
    {
        RuleFor(v => v.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl is required.")
            .Must(BeAValidUrl).WithMessage("ImageUrl must be a valid URL.");

        RuleFor(v => v.SomeEventId)
            .NotEmpty().WithMessage("SomeEventId is required.")
            .NotEqual(Guid.Empty).WithMessage("SomeEventId cannot be empty GUID.");
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
