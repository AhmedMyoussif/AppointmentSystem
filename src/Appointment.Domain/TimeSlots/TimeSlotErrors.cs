using Appointment.Domain.Common;

namespace Appointment.Domain.TimeSlots;

public static class TimeSlotErrors
{
    public static readonly Error TimeSlotNotFound = Error.Validation("Time slot not found.");
    public static readonly Error InvalidTimeSlot = Error.Validation("Invalid time slot.");
    public static readonly Error ProviderIdCannotBeEmpty = Error.Validation("Provider ID cannot be empty.");
    public static readonly Error StartTimeCannotBeInThePast = Error.Validation("Start time cannot be in the past.");
    public static readonly Error StartTimeMustBeBeforeEndTime = Error.Validation("Start time must be before end time.");
}