using FluentValidation;

namespace Appointment.Application.Features.TimeSlots.Commands.CreateTimeSlot;

public sealed class CreateTimeSlotValidator : AbstractValidator<CreateTimeSlotCommand>
{
    public CreateTimeSlotValidator()
    {
        RuleFor(x => x.ProviderId).NotEmpty().WithMessage("Provider ID is required.");
        RuleFor(x => x.StartTime).NotEmpty().WithMessage("Start time is required.");
        RuleFor(x => x.EndTime).NotEmpty().WithMessage("End time is required.");
    }
}