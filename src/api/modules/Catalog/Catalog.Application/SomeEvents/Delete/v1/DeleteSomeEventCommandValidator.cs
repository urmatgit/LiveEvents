using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.SomeEvents.Delete.v1;

public class DeleteSomeEventCommandValidator : AbstractValidator<DeleteSomeEventCommand>
{
    public DeleteSomeEventCommandValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty();
    }
}
