using FluentValidation;

namespace FSH.Starter.WebApi.Catalog.Application.EventImages.Delete.v1;

public class DeleteEventImageValidator : AbstractValidator<DeleteEventImageCommand>
{
    public DeleteEventImageValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("EventImage ID is required.")
            .NotEqual(Guid.Empty).WithMessage("EventImage ID cannot be empty GUID.");
    }
}
