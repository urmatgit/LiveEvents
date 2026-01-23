using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Update.v1;

public class UpdateSomeEventCommandValidator : AbstractValidator<UpdateSomeEventCommand>
{
    public UpdateSomeEventCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .Length(2, 100);
            
        RuleFor(e => e.Description)
            .MaximumLength(1000);
        RuleFor(e => e.MinParticipant)
        .GreaterThanOrEqualTo(1);
        RuleFor(e => e.MaxParticipant)
            .GreaterThanOrEqualTo(e => e.MinParticipant);
        RuleFor(e => e.Price)
            .GreaterThanOrEqualTo(0);
            
        RuleFor(e => e.EventCatalogId)
            .NotEmpty();
    }
}
