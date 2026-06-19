using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.TimeSlots.Queries.GetProviderTimeSlots;

public sealed class GetProviderTimeSlotsQueryHandler 
    : IRequestHandler<GetProviderTimeSlotsQuery, Result<List<TimeSlotDto>>>
{
    private readonly IAppDbContext _context;

    public GetProviderTimeSlotsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<TimeSlotDto>>> Handle(GetProviderTimeSlotsQuery request, CancellationToken ct)
    {
        var slots = await _context.TimeSlots
            .AsNoTracking()
            .Where(ts => ts.ProviderId == request.ProviderId)
            .OrderBy(ts => ts.StartTime) 
            .Select(ts => new TimeSlotDto(ts.Id, ts.ProviderId, ts.StartTime, ts.EndTime, ts.IsBooked))
            .ToListAsync(ct);

        return slots;
    }
}