using FluentValidation;

namespace Appointment.Application.Features.Categories.Commands.UpdateCategory;

public sealed class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Category ID is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required.");
    }
}