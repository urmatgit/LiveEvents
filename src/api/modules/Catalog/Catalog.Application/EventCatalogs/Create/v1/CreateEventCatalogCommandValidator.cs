using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Create.v1;

public class CreateEventCatalogCommandValidator : AbstractValidator<CreateEventCatalogCommand>
{
    public CreateEventCatalogCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .Length(2, 100);
    }
}
