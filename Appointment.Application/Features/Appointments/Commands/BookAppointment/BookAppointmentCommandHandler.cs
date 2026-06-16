using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Appointments.Commands.BookAppointment;

public sealed class BookAppointmentCommandHandler
    : IRequestHandler<BookAppointmentCommand, Result<Guid>>
{

    private readonly IAppDbContext _context;

    public BookAppointmentCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<Result<Guid>> Handle(BookAppointmentCommand request,
                                           CancellationToken ct)
    {
       var Customer = await _context.Users.AnyAsync(x => x.Id == request.CustomerId , ct);

       if (!Customer)
       {
            return ApplicationErrors.UserNotFound;
       }

       var service = await _context.Services
       .AsNoTracking()
       .FirstOrDefaultAsync(s => s.Id == request.ServiceId, ct);

       if (service is null)
       {
            return ApplicationErrors.ServiceNotFound;
       }

       var timeSlot = await _context.TimeSlots
       .AsNoTracking()
       .FirstOrDefaultAsync(ts => ts.Id == request.TimeSlotId, ct);

        if (timeSlot is null)
        {
           return ApplicationErrors.TimeSlotNotFound;
        }

        var appointmentResult = Domain.Appointment.Create(Guid.NewGuid(),
                                                          request.CustomerId,
                                                          request.ServiceId,
                                                          request.TimeSlotId,
                                                          timeSlot.StartTime.Date,
                                                          timeSlot.StartTime.TimeOfDay,
                                                          timeSlot.EndTime.TimeOfDay,
                                                          service.Price,
                                                          request.Notes ?? string.Empty);
        if (appointmentResult.IsError)
        {
            return appointmentResult.Errors;
        }

        timeSlot.MarkAsBooked();

        _context.Appointments.Add(appointmentResult.Value);
        
        await _context.SaveChangesAsync(ct);

        return appointmentResult.Value.Id;
    }
}