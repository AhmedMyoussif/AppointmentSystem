namespace Appointment.Application.Features.TimeSlots.Queries;

public sealed record TimeSlotDto(Guid Id,
                                 Guid ProviderId,
                                 DateTime StartTime,
                                 DateTime EndTime,
                                 bool IsBooked);