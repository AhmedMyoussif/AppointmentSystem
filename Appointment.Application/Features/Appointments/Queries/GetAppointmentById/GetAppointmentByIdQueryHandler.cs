using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using Appointment.Application.Features.Appointments.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Appointments.Queries.GetAppointmentById;

public sealed class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, Result<AppointmentDto>>
{
    private readonly IAppDbContext _context;

    public GetAppointmentByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<Result<AppointmentDto>> Handle(GetAppointmentByIdQuery request,
                                                     CancellationToken ct)
    {
        var appointmentDto = await _context.Appointments
            .AsNoTracking() 
            .Where(a => a.Id == request.AppointmentId)
            .Select(a => new AppointmentDto(
                a.Id,
                a.CustomerId,
                _context.Users.Where(u => u.Id == a.CustomerId).Select(u => u.FirstName).FirstOrDefault() ?? "Unknown Customer", 
                a.ServiceId,
                _context.Services.Where(s => s.Id == a.ServiceId).Select(s => s.Name).FirstOrDefault() ?? "Unknown Service",
                a.AppointmentDate,
                a.StartTime,
                a.EndTime,
                a.Status.ToString(), 
                a.PriceAtBooking,
                a.Notes
            ))
            .FirstOrDefaultAsync(ct);

        if (appointmentDto is null)
        {
            return ApplicationErrors.AppointmentNotFound;
        }

        return appointmentDto; 
    }
}