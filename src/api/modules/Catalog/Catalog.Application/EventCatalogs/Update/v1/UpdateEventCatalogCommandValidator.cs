using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Update.v1;

public class UpdateEventCatalogCommandValidator : AbstractValidator<UpdateEventCatalogCommand>
{
    public UpdateEventCatalogCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .Length(2, 100);
    }
}
