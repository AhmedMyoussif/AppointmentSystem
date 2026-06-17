
using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Appointments.Queries;

public sealed class GetCustomerAppoinmentsQueryHandler : IRequestHandler<GetCustomerAppoinmentsQuery, Result<List<AppointmentDto>>>
{
    public readonly IAppDbContext _context;

    public GetCustomerAppoinmentsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<AppointmentDto>>> Handle(GetCustomerAppoinmentsQuery request, CancellationToken ct)
    {
        var customerExists = await _context.Users
            .AnyAsync(u => u.Id == request.CustomerId, ct);

        if (!customerExists)
        {
            return ApplicationErrors.UserNotFound;
        }
        var appointments = await _context.Appointments
            .AsNoTracking()
            .Where(a => a.CustomerId == request.CustomerId)
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