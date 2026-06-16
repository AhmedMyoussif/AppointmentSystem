using Appointment.Domain.Common;
using Appointment.Domain.Common.Results;
using Appointment.Domain.Services;
using Appointment.Domain.Users;

namespace Appointment.Domain; 

public class Appointment : AuditableEntity
{
    
    // Foreign Keys
    public Guid CustomerId { get; private set; }
    public Guid ServiceId { get; private set; }
    public Guid TimeSlotId { get; private set; }

    // Navigation Properties
    public User Customer { get; private set; }
    public Service Service { get; private set; }

    // Date & Time
    public DateTime AppointmentDate { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }

    // Business Data
    public AppointmentStatus Status { get; private set; }
    public decimal PriceAtBooking { get; private set; } 
    public string Notes { get; private set; } = string.Empty;

    // Constructor
    private Appointment()
     { }

    private Appointment(Guid id,
                        Guid customerId,
                        Guid serviceId,
                        Guid timeSlotId,
                        DateTime appointmentDate,
                        TimeSpan startTime,
                        TimeSpan endTime,
                        decimal priceAtBooking,
                        string notes) : base(id)
    {
        CustomerId = customerId;
        ServiceId = serviceId;
        TimeSlotId = timeSlotId;
        AppointmentDate = appointmentDate;
        StartTime = startTime;
        EndTime = endTime;
        PriceAtBooking = priceAtBooking;
        Notes = notes;
        Status = AppointmentStatus.Pending; 
    }

    // Factory Method

    public static Result<Appointment> Create(Guid id,
                                             Guid customerId,
                                             Guid serviceId,
                                             Guid timeSlotId,
                                             DateTime appointmentDate,
                                             TimeSpan startTime,
                                             TimeSpan endTime,
                                             decimal priceAtBooking,
                                             string notes)
    {
        if (id == Guid.Empty)
            return AppointmentErrors.TimeSlotIdCannotBeEmpty; 

        if (customerId == Guid.Empty)
            return AppointmentErrors.CustomerIdCannotBeEmpty;

        if (serviceId == Guid.Empty)
            return AppointmentErrors.ServiceIdCannotBeEmpty;

        if (timeSlotId == Guid.Empty)
            return AppointmentErrors.TimeSlotIdCannotBeEmpty;

        if (appointmentDate.Date < DateTime.UtcNow.Date)
            return AppointmentErrors.AppointmentDateCannotBeInThePast;

        if (startTime >= endTime)
            return AppointmentErrors.StartTimeMustBeBeforeEndTime;

        if (priceAtBooking < 0)
            return AppointmentErrors.PriceCannotBeNegative;

        var safeNotes = string.IsNullOrWhiteSpace(notes) ? string.Empty : notes.Trim();

        var appointment = new Appointment(id,
                                          customerId,
                                          serviceId,
                                          timeSlotId,
                                          appointmentDate,
                                          startTime,
                                          endTime,
                                          priceAtBooking,
                                          safeNotes);

        return appointment;
    }
    public Result<Success> Approve()
    {
        if (Status != AppointmentStatus.Pending)
        {
            return AppointmentErrors.InvalidStatusTransition(Status.ToString(), AppointmentStatus.Confirmed.ToString());
        }

        Status = AppointmentStatus.Confirmed;
        return default(Success);
    }

    public Result<Success> Reject()
    {
        if (Status != AppointmentStatus.Pending)
        {
            return AppointmentErrors.InvalidStatusTransition(Status.ToString(), AppointmentStatus.Rejected.ToString());
        }

        Status = AppointmentStatus.Rejected;
        return default(Success); 
    }

    public Result<Success> Complete()
    {   
        if (Status != AppointmentStatus.Confirmed)
        {
            return AppointmentErrors.InvalidStatusTransition(Status.ToString(), AppointmentStatus.Completed.ToString());
        }

        if (AppointmentDate.Date > DateTime.UtcNow.Date)
        {
            return AppointmentErrors.InvalidStatusTransition(Status.ToString(), "Completed (Cannot complete a future appointment)");
        }

        Status = AppointmentStatus.Completed;
        return default(Success);
    }
    public Result<Success> Cancel()
    {
        if (Status == AppointmentStatus.Completed || Status == AppointmentStatus.Rejected || Status == AppointmentStatus.Cancelled)
        {
            return AppointmentErrors.InvalidStatusTransition(Status.ToString(), AppointmentStatus.Cancelled.ToString());
        }

        if (AppointmentDate.Date < DateTime.UtcNow.Date)
        {
            return AppointmentErrors.CannotCancelPastAppointment;
        }

        Status = AppointmentStatus.Cancelled;
        return default(Success);
    }

}