using FluentValidation;

namespace Appointment.Application.Features.Appointments.Commands.CancelAppointment;

public sealed class CancelAppointmentValidator : AbstractValidator<CancelAppointmentCommand>
{
    public CancelAppointmentValidator()
    {
        RuleFor(x => x.AppointmentId)
            .NotEmpty()
            .WithMessage("Appointment ID is required.");

        RuleFor(x => x.CancellationReason)
            .MaximumLength(300)
            .WithMessage("Cancellation reason cannot exceed 300 characters.");
    }
}