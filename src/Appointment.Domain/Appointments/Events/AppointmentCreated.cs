using Appointment.Domain.Common;

namespace Appointment.Domain.Appointments.Events;


public sealed class AppointmentCreated : DomainEvent
{
    public Guid AppointmentId { get; init; }

    public AppointmentCreated(Guid appointmentId)
    {
        AppointmentId = appointmentId;
    }
}