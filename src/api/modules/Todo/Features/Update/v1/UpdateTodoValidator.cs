using FluentValidation;

namespace FSH.Starter.WebApi.Todo.Features.Update.v1;
public class UpdateTodoValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Title).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Note).MaximumLength(500);
    }
}
