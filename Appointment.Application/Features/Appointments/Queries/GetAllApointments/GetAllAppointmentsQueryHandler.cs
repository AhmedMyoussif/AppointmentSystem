using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Appointments.Queries;

public sealed class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, Result<List<AppointmentDto>>>
{
    private readonly IAppDbContext _context;

    public GetAllAppointmentsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<AppointmentDto>>> Handle(GetAllAppointmentsQuery request, CancellationToken ct)
    {
        var appointments = await _context.Appointments
            .AsNoTracking()
            .OrderByDescending(a => a.AppointmentDate) 
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
            .ToListAsync(ct);

        return appointments;
    }
}