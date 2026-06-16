using Appointment.Domain.Common;

namespace Appointment.Domain;

public static class AppointmentErrors
{
    // 1. Validation Errors 
    public static readonly Error CustomerIdCannotBeEmpty = 
        Error.Validation("Appointment.CustomerIdEmpty", "Customer ID cannot be empty.");
        
    public static readonly Error ServiceIdCannotBeEmpty = 
        Error.Validation("Appointment.ServiceIdEmpty", "Service ID cannot be empty.");
        
    public static readonly Error TimeSlotIdCannotBeEmpty = 
        Error.Validation("Appointment.TimeSlotIdEmpty", "Time Slot ID cannot be empty.");
        
    public static readonly Error AppointmentDateCannotBeInThePast = 
        Error.Validation("Appointment.DateInPast", "Appointment date cannot be in the past.");
        
    public static readonly Error StartTimeMustBeBeforeEndTime = 
        Error.Validation("Appointment.InvalidTimeRange", "Start time must be before end time.");

    public static readonly Error PriceCannotBeNegative = 
        Error.Validation("Appointment.PriceNegative", "Booking price cannot be negative.");


    // 2. Business Logic Errors 
    public static readonly Error TimeSlotUnavailable = 
        Error.Failure("Appointment.TimeSlotUnavailable", "The selected time slot is already booked or unavailable.");

    public static readonly Error ServiceNotFound = 
        Error.NotFound("Appointment.ServiceNotFound", "The selected service does not exist.");

    public static readonly Error CustomerNotFound = 
        Error.NotFound("Appointment.CustomerNotFound", "The specified customer does not exist.");

    public static readonly Error AppointmentNotFound = 
        Error.NotFound("Appointment.NotFound", "The specified appointment does not exist.");


    // 3. State & Actions Errors
    public static readonly Error CannotCancelPastAppointment = 
        Error.Failure("Appointment.CannotCancelPast", "Cannot cancel an appointment that has already passed.");

    public static readonly Error CannotReschedulePastAppointment = 
        Error.Failure("Appointment.CannotReschedulePast", "Cannot reschedule an appointment that has already passed.");

    public static Error InvalidStatusTransition(string fromStatus, string toStatus) => 
        Error.Failure(
            "Appointment.InvalidTransition", 
            $"Cannot change appointment status from '{fromStatus}' to '{toStatus}'.");
}