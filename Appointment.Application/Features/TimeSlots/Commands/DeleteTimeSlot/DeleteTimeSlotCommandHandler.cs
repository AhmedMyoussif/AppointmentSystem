using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.TimeSlots.Commands.DeleteTimeSlot;

public sealed class DeleteTimeSlotCommandHandler : IRequestHandler<DeleteTimeSlotCommand, Result<Deleted>>
{
    private readonly IAppDbContext _context;

    public DeleteTimeSlotCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Deleted>> Handle(DeleteTimeSlotCommand request, CancellationToken ct)
    {
        
        var timeSlot = await _context.TimeSlots
            .FirstOrDefaultAsync(ts => ts.Id == request.Id, ct);

        if (timeSlot is null)
        {
            return ApplicationErrors.TimeSlotNotFound; 
        }

        if (timeSlot.IsBooked)
        {
            return ApplicationErrors.CannotDeleteBookedTimeSlot; 
        }

        _context.TimeSlots.Remove(timeSlot);
        await _context.SaveChangesAsync(ct);

        return Result.Deleted;
    }
}