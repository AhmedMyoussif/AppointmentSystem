using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.TimeSlots.Queries.GetAvailableTimeSlots;

public sealed class GetAvailableTimeSlotsQueryHandler 
    : IRequestHandler<GetAvailableTimeSlotsQuery, Result<List<TimeSlotDto>>>
{
    private readonly IAppDbContext _context;

    public GetAvailableTimeSlotsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<TimeSlotDto>>> Handle(GetAvailableTimeSlotsQuery request, CancellationToken ct)
    {
        var availableSlots = await _context.TimeSlots
            .AsNoTracking()
            .Where(ts => ts.ProviderId == request.ProviderId && 
                         !ts.IsBooked && 
                         ts.StartTime > DateTime.UtcNow) 
            .OrderBy(ts => ts.StartTime)
            .Select(ts => new TimeSlotDto(ts.Id, ts.ProviderId, ts.StartTime, ts.EndTime, ts.IsBooked))
            .ToListAsync(ct);

        return availableSlots;
    }
}