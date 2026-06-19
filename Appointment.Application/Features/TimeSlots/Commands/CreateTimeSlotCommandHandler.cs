using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using Appointment.Domain.TimeSlots;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.TimeSlots.Commands.CreateTimeSlot;

public sealed class CreateTimeSlotCommandHandler 
    : IRequestHandler<CreateTimeSlotCommand, Result<Guid>>
{
    private readonly IAppDbContext _context;

    public CreateTimeSlotCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateTimeSlotCommand request, CancellationToken ct)
    {
        var providerExists = await _context.Users
            .AnyAsync(u => u.Id == request.ProviderId, ct);

        if (!providerExists)
        {
            return ApplicationErrors.ProviderNotFound;
        }

        var hasOverlap = await _context.TimeSlots
            .AnyAsync(ts => ts.ProviderId == request.ProviderId &&
                            ((request.StartTime >= ts.StartTime && request.StartTime < ts.EndTime) || 
                             (request.EndTime > ts.StartTime && request.EndTime <= ts.EndTime) ||
                             (request.StartTime <= ts.StartTime && request.EndTime >= ts.EndTime)), ct);

        if (hasOverlap)
        {
            return ApplicationErrors.TimeSlotOverlapsWithExisting;
        }

        var timeSlotResult = TimeSlot.Create(
            Guid.NewGuid(),
            request.ProviderId,
            request.StartTime,
            request.EndTime
        );

        if (timeSlotResult.IsError)
        {
            return timeSlotResult.Errors;
        }

        var timeSlot = timeSlotResult.Value;

        _context.TimeSlots.Add(timeSlot);
        await _context.SaveChangesAsync(ct);

        return timeSlot.Id;
    }
}