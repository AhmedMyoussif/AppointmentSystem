using Appointment.Domain.Common;
using Appointment.Domain.Common.Results;

namespace Appointment.Domain.TimeSlots; 

public class TimeSlot : AuditableEntity
{
    public Guid ProviderId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public bool IsBooked { get; private set; }



    private TimeSlot() { }

    private TimeSlot(Guid id, Guid providerId, DateTime startTime, DateTime endTime) : base(id)
    {
        ProviderId = providerId;
        StartTime = startTime;
        EndTime = endTime;
        IsBooked = false;
    }

    public static Result<TimeSlot> Create(Guid id, Guid providerId, DateTime startTime, DateTime endTime)
    {
        if (providerId == Guid.Empty)
            return TimeSlotErrors.ProviderIdCannotBeEmpty;

        if (startTime < DateTime.UtcNow)
            return TimeSlotErrors.StartTimeCannotBeInThePast;

        if (startTime >= endTime)
            return TimeSlotErrors.StartTimeMustBeBeforeEndTime;

        return new TimeSlot(id, providerId, startTime, endTime);
    }

    public void MarkAsBooked()
    {
        IsBooked = true;
    }
    public void Release()
    {
        IsBooked = false;
    }

}