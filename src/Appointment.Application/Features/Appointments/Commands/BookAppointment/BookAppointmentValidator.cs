using FluentValidation;

namespace Appointment.Application.Features.Appointments.Commands.BookAppointment;

public sealed class BookAppointmentValidator : AbstractValidator<BookAppointmentCommand>
{
    public BookAppointmentValidator()
    {
        
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required and cannot be empty.");

        
        RuleFor(x => x.ServiceId)
            .NotEmpty()
            .WithMessage("Service ID is required and cannot be empty.");

        RuleFor(x => x.TimeSlotId)
            .NotEmpty()
            .WithMessage("Time slot ID is required and cannot be empty.");
            
        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .WithMessage("Notes cannot exceed 500 characters.");
    }
}