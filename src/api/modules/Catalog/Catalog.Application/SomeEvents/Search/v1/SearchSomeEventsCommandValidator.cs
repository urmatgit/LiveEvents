using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Search.v1;

public class SearchSomeEventsCommandValidator : AbstractValidator<SearchSomeEventsCommand>
{
    public SearchSomeEventsCommandValidator()
    {
        RuleFor(e => e.Name)
            .MaximumLength(100);
            
        RuleFor(e => e.Description)
            .MaximumLength(1000);
            
        RuleFor(e => e.EventCatalogId)
            .NotEmpty();
    }
}
