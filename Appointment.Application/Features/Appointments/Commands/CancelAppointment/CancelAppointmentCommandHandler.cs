using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Appointments.Commands.CancelAppointment;

public sealed class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, Result<Success>>
{
    private readonly IAppDbContext _context;

    public CancelAppointmentCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Success>> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == request.AppointmentId, cancellationToken);

        if (appointment is null)
        {
            return ApplicationErrors.AppointmentNotFound; 
        }

        var timeSlot = await _context.TimeSlots
            .FirstOrDefaultAsync(ts => ts.Id == appointment.TimeSlotId, cancellationToken);

        if (timeSlot is null)
        {
            return ApplicationErrors.TimeSlotNotFound;
        }

        var cancelResult = appointment.Cancel(); 
        if (cancelResult.IsError)
        {
            return cancelResult.Errors; 
        }

        timeSlot.Release();

        await _context.SaveChangesAsync(cancellationToken);

        return new Success(); 
    }
}